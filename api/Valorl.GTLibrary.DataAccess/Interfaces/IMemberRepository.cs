using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valorl.GTLibrary.Models;

namespace Valorl.GTLibrary.DataAccess.Interfaces
{
    public interface IMemberRepository
    {
        Task<DbMember> Create(DbMember member);
        Task<DbMember> GetOneBySsn(string ssn);
    }
}
