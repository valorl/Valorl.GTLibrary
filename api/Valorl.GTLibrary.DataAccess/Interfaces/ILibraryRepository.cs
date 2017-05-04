using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valorl.GTLibrary.Models;

namespace Valorl.GTLibrary.DataAccess.Interfaces
{
    public interface ILibraryRepository
    {
        Task<DbLibrary> GetOne(Guid id);
        Task<DbLibrary> Create(Guid id, string name, DbAddress address);
    }
}
