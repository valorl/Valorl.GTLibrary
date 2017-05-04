using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valorl.GTLibrary.Models;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.DataAccess.Interfaces
{
    public interface IAcquirementRepository
    {
        Task Create(Guid id, DateTime acquirementDateUtc, EDbAcquirementStatus status, string isbn,
                    int[] copyNumbers, Guid receivingId, Guid givingId);

        Task<DbAcquirement> GetOne(Guid id);
    }
}
