using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Valorl.GTLibrary.SystemTests.Utils
{
    public class DatabaseTestHelper
    {
        public async Task DeleteItem(string isbn)
        {
            await SqlExecutor.ExecuteSql($"DELETE FROM Items WHERE ISBN = '{isbn}'");
        }
        public async Task DeleteLibrary(Guid id)
        {
            await SqlExecutor.ExecuteSql($"DELETE FROM Libraries WHERE Id = '{id}'");
        }
        public async Task DeleteAddress(int id)
        {
            await SqlExecutor.ExecuteSql($"DELETE FROM Addresses WHERE Id = '{id}'");
        }
        public async Task DeleteAcquirement(Guid id)
        {
            await SqlExecutor.ExecuteSql($"DELETE FROM ItemAcquirements WHERE Id = '{id}'");
        }
    }
}
