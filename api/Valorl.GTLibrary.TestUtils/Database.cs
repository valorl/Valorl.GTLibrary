using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Valorl.GTLibrary.TestUtils
{
    public class Database
    {
        private readonly string _connectionString;

        public Database()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            _connectionString = config.GetConnectionString("GTLibrary");
        }

        public async Task ExecuteAsync(string sql, CommandType cmdType = CommandType.Text)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.ExecuteAsync(sql, commandType: cmdType);
            }
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object parameters = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<dynamic>(sql, param: parameters);
                return result;
            }
        } 
    }
}
