using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Valorl.GTLibrary.DataAccess.Interfaces;
using Valorl.GTLibrary.Models;

namespace Valorl.GTLibrary.DataAccess
{
    public class ItemRepository : IItemRepository
    {
        private readonly string _connectionString;

        public ItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<DbItem>> GetAll()
        {
            const string query = @"SELECT i.*, sa.*
                                   FROM Items i
                                   INNER JOIN SubjectAreas sa
                                   ON i.SubjectArea_Id = sa.Id";
            using (var conn = new SqlConnection(_connectionString))
            {
                var items = await conn.QueryAsync<DbItem, DbSubjectArea, DbItem>(query, (item, area) =>
                {
                    item.SubjectArea = area;
                    return item;
                });
                return items;
            }
        }

        public async Task<DbItem> GetOneByIsbn(string isbn)
        {
            const string query = @"SELECT i.*, sa.*
                                   FROM Items i
                                   INNER JOIN SubjectAreas sa
                                   ON i.ISBN = @isbn
                                   AND i.SubjectArea_Id = sa.Id";
            using (var conn = new SqlConnection(_connectionString))
            {
                var dbItem = (await conn.QueryAsync<DbItem,DbSubjectArea, DbItem>(query, (item, area) =>
                {
                    item.SubjectArea = area;
                    return item;
                }, new { isbn })).SingleOrDefault();

                return dbItem;
            }
        }

        public async Task<DbItem> Create(DbItem item)
        {
            const string query = @"INSERT INTO Items (ISBN,Author,Title,Description,IsLendable,SubjectArea_Id)
                                   VALUES (@isbn, @author, @title, @description, @isLendable, @subjectAreaId)";
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.ExecuteAsync(query, new
                {
                    isbn = item.ISBN,
                    author = item.Author,
                    title = item.Title,
                    description = item.Description,
                    isLendable = item.IsLendable,
                    subjectAreaId = item.SubjectArea.Id
                });

                return item;
            }
        }
    }
}
