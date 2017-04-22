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
    public static class ItemCopyMapping
    {
        public static ItemCopyDto ToDto(this DbItemCopy copy)
        {
            var dto = new ItemCopyDto()
            {
                Number = copy.Number,
                ISBN = copy.ISBN,
                IsAvailable = copy.IsAvailable,
                Type = (EItemCopyTypeDto)(int) copy.Type
            };
            return dto;
        }

        public static DbItemCopy ToDbModel(this ItemCopyDto dto)
        {
            var copy = new DbItemCopy()
            {
                Number = dto.Number,
                ISBN = dto.ISBN,
                IsAvailable = dto.IsAvailable,
                Type = (EDbItemCopyType)(int)dto.Type
            };
            return copy;    
        }
    }
}
