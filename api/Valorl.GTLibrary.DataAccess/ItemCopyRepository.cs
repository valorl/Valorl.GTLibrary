using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Valorl.GTLibrary.DataAccess.Interfaces;
using Valorl.GTLibrary.Models;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.DataAccess
{
    public class ItemCopyRepository : IItemCopyRepository
    {
        private readonly string _connectionString;

        public ItemCopyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<DbItemCopy>> GetMany(string isbn)
        {
            const string query = @"SELECT *
                                   FROM ItemCopies
                                   WHERE ISBN = @isbn";
            using (var conn = new SqlConnection(_connectionString))
            {
                var copies = await conn.QueryAsync<DbItemCopy>(query, new {isbn});
                return copies;
            }
        }

        public async Task<DbItemCopy> GetOne(int number, string isbn)
        {
            const string query = @"SELECT *
                                   FROM ItemCopies
                                   WHERE Number = @number
                                   AND ISBN = @isbn";
            using (var conn = new SqlConnection(_connectionString))
            {
                var copies = await conn.QueryAsync<DbItemCopy>(query, new {number, isbn});
                var copy = copies.Single();
                return copy;
            }
        }

        public async Task<DbItemCopy> Create(int number, string isbn, bool isAvailable, EDbItemCopyType type)
        {
            const string query = @"INSERT INTO ItemCopies
                                   VALUES (@number, @isbn, @isAvailable, @type)";
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.ExecuteAsync(query, new {number, isbn, isAvailable, type});

                var createdCopy = new DbItemCopy()
                {
                    Number = number,
                    ISBN = isbn,
                    IsAvailable = isAvailable,
                    Type = type
                };
                return createdCopy;
            }
        }

        public async Task<IEnumerable<DbItemCopy>> CreateMany(IEnumerable<DbItemCopy> copies)
        {
            const string query = @"INSERT INTO ItemCopies
                                   VALUES (@number, @isbn, @isAvailable, @type)";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var copy in copies)
                        {
                            await conn.ExecuteAsync(query, new
                            {
                                number = copy.Number,
                                isbn = copy.ISBN,
                                isAvailable = copy.IsAvailable,
                                type = copy.Type
                            }, trans);
                        }
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                    }
                }
                return copies;
            }
        }

    }
}

