using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valorl.GTLibrary.Models;

namespace Valorl.GTLibrary.DataAccess.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<DbItem>> GetAll();
        Task<DbItem> GetOneByIsbn(string isbn);
        Task<DbItem> Create(DbItem item);
    }
}

