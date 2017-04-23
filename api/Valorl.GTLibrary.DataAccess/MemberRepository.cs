using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Valorl.GTLibrary.DataAccess.Interfaces;
using Valorl.GTLibrary.Models;

namespace Valorl.GTLibrary.DataAccess
{
    public class MemberRepository : IMemberRepository
    {
        private readonly string _connectionString;

        public MemberRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<DbMember> Create(DbMember member)
        {
            const string memberQuery = @"INSERT INTO Members (SSN, FirstName, LastName, Type, PhoneNr, Email)
                                         VALUES (@ssn, @firstName, @lastName, @type, @phoneNr, @email)";
            const string addressQuery = @"INSERT INTO MemberAddresses (Street, Number, AddressType, Country, ZipCode, City, Member_SSN)
                                          OUTPUT INSERTED.Id
                                          VALUES (@street, @number, @addressType, @country, @zipCode, @city, @memberSsn";

            var addresses = new List<DbAddress>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync(memberQuery, new
                        {
                            ssn = member.SSN,
                            firstName = member.FirstName,
                            lastName = member.LastName,
                            type = member.Type,
                            phoneNr = member.PhoneNr,
                            email = member.Email
                        }, trans);

                        foreach (var address in member.Addresses)
                        {
                            var addressId = await conn.ExecuteScalarAsync<int>(addressQuery, new
                            {
                                street = address.Street,
                                number = address.Number,
                                addressType = address.Type,
                                country = address.Country,
                                zipCode = address.ZipCode,
                                city = address.City,
                                memberSsn = member.SSN
                            }, trans);

                            addresses.Add(GetAddressWithId(addressId, address));
                        }
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                    }
                }
            }
            member.Addresses = addresses;
            return member;
        }

        public async Task<DbMember> GetOneBySsn(string ssn)
        {
            const string memberQuery = @"SELECT SSN, FirstName, MiddleName, LastName, Type, PhoneNr, Email, 
	                                     FROM Members
                                         WHERE SSN = @ssn";
            const string addressQuery = @"SELECT Id, Street, Number, AddressType, Country, ZipCode, City
                                          FROM MemberAddresses
                                          WHERE Member_SSN = @ssn";

            using (var conn = new SqlConnection(_connectionString))
            {
                var member = await conn.QuerySingleAsync<DbMember>(memberQuery, new {ssn});
                var addresses = await conn.QueryAsync<DbAddress>(addressQuery, new {ssn});

                member.Addresses = addresses;
                return member;
            }
        }

        private DbAddress GetAddressWithId(int id, DbAddress address)
        {
            return new DbAddress()
            {
                Id = id,
                Street = address.Street,
                Number = address.Number,
                Type = address.Type,
                Country = address.Country,
                ZipCode = address.ZipCode,
                City = address.City
            };
        }
    }
}
