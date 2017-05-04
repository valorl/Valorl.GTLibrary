using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.DTOs.Enums;
using Valorl.GTLibrary.Models;

namespace Valorl.GTLibrary.Api.Mappings
{
    public static class AcquirementMapping
    {
        public static AcquirementDto ToDto(this DbAcquirement acq, DbItem item, IEnumerable<DbItemCopy> copies,
                                           DbLibrary receiver, DbLibrary sender)
        {
            return new AcquirementDto()
            {
                Id = acq.Id,
                DateUtc = acq.AcquirementDateUtc,
                Status = (EAcquirementStatusDto)(int)acq.Status,
                Item = item.ToDto(),
                Receiver = receiver.ToDto(),
                Sender = sender.ToDto(),
                ItemCopies = copies.Select(x => x.ToDto())
            };
        }
    }
}
