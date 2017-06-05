using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Valorl.GTLibrary.DataAccess.Interfaces;
using Valorl.GTLibrary.Models;

namespace Valorl.GTLibrary.DataAccess
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly string _connectionString;

        public LibraryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<DbLibrary> GetOne(Guid id)
        {
            const string query = @"SELECT l.Id, l.Name, a.Id, a.Street, a.Number, a.AddressType, a.Country, a.ZipCode, a.City 
                                   FROM Libraries l
                                   INNER JOIN Addresses a
                                   ON l.Id = @id
                                   AND l.Address_Id = a.Id";
            using (var conn = new SqlConnection(_connectionString))
            {
                var dbLibrary = (await conn.QueryAsync<DbLibrary, DbAddress, DbLibrary>(query, (library, address) =>
                    {
                        library.Address = address;
                        return library;
                    }, new {id}))
                    .SingleOrDefault();
                return dbLibrary;
            }
        }

        public async Task<DbLibrary> Create(Guid id, string name, DbAddress address)
        {
            const string queryAddress = @"INSERT INTO Addresses (Street, Number, AddressType, Country, ZipCode, City)
                                          OUTPUT INSERTED.Id
                                          VALUES (@street, @number, @addressType, @country, @zipCode, @city)";
            const string queryLibrary = @"INSERT INTO Libraries (Id,Name,Address_Id)
                                          VALUES(@id, @name, @addressId)";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var addressId = await conn.ExecuteScalarAsync<int>(queryAddress, new
                        {
                            street = address.Street,
                            number = address.Number,
                            addressType = address.Type,
                            country = address.Country,
                            zipCode = address.ZipCode,
                            city = address.City
                        }, trans);
                        address.Id = addressId;

                        await conn.ExecuteAsync(queryLibrary, new {id, name, addressId}, trans);
                        trans.Commit();

                        var dbLibrary = new DbLibrary()
                        {
                            Id = id,
                            Name = name,
                            Address = address
                        };
                        return dbLibrary;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

