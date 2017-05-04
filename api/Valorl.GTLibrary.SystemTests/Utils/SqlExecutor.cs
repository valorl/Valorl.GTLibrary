using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Valorl.GTLibrary.SystemTests.Utils
{
    public static class SqlExecutor
    {
        public static async Task ExecuteSql(string query)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            using (var conn = new SqlConnection(config.GetConnectionString("GTLibrary")))
            {
                await conn.ExecuteAsync(query);
            }
        }
    }
}
