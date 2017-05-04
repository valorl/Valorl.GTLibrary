using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.Models;

namespace Valorl.GTLibrary.Api.Mappings
{
    public static class LibraryMapping
    {
        public static LibraryDto ToDto(this DbLibrary lib)
        {
            return new LibraryDto()
            {
                Id = lib.Id,
                Name = lib.Name,
                Address = lib.Address.ToDto()
            };
        }

        public static DbLibrary ToDbModel(this LibraryDto dto)
        {
            return new DbLibrary()
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address.ToDbModel()
            };
        }
    }
}
