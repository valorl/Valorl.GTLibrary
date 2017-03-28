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
        Task<string> Create(string isbn, string author, string title, 
                      string description, bool isLendable, int subjectAreaId);

    }
}

