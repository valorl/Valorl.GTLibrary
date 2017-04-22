using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valorl.GTLibrary.Models;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.DataAccess.Interfaces
{
    public interface IItemCopyRepository
    {
        Task<IEnumerable<DbItemCopy>> GetMany(string isbn);
        Task<DbItemCopy> GetOne(int number, string isbn);
        Task<DbItemCopy> Create(int number, string isbn, bool isAvailable, EDbItemCopyType type);
        Task<IEnumerable<DbItemCopy>> CreateMany(IEnumerable<DbItemCopy> copies);
    }
}
