using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.DTOs.Enums;
using Valorl.GTLibrary.Models;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.Api.Mappings
{
    public static class MemberMapping
    {
        public static MemberDto ToDto(this DbMember dbMember)
        {
            var dto = new MemberDto()
            {
                SSN = dbMember.SSN,
                FirstName = dbMember.FirstName,
                MiddleName = dbMember.MiddleName,
                LastName = dbMember.LastName,
                Type = (EMemberTypeDto)(int)dbMember.Type,
                PhoneNr = dbMember.PhoneNr,
                Email = dbMember.Email,
                Addresses = dbMember.Addresses.Select(x => x.ToDto())
            };
            return dto;
        }

        public static DbMember ToDbModel(this MemberDto dto)
        {
            var dbMember = new DbMember()
            {
                SSN = dto.SSN,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Type = (EDbMemberType)(int)dto.Type,
                PhoneNr = dto.PhoneNr,
                Email = dto.Email,
                Addresses = dto.Addresses.Select(x => x.ToDbModel())
            };
            return dbMember;
        }
    }
}
