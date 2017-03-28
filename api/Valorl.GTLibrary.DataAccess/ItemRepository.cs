using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public async Task<string> Create(string isbn, string author, string title, string description, bool isLendable, int subjectAreaId)
        {
            throw new NotImplementedException();
        }
    }
}
