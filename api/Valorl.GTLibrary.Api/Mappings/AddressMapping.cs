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
    public static class AddressMapping
    {
        public static AddressDto ToDto(this DbAddress dbAddress)
        {
            var dto = new AddressDto()
            {
                Id = dbAddress.Id,
                Street = dbAddress.Street,
                Number = dbAddress.Number,
                City = dbAddress.City,
                Country = dbAddress.Country,
                ZipCode = dbAddress.ZipCode,
                Type = (EAddressTypeDto)(int)dbAddress.Type
            };
            return dto;
        }

        public static DbAddress ToDbModel(this AddressDto dto)
        {
            var dbAddress = new DbAddress()
            {
                Id = dto.Id,
                Street = dto.Street,
                Number = dto.Number,
                City = dto.City,
                Country = dto.Country,
                ZipCode = dto.ZipCode,
                Type = (EDbAddressType)(int)dto.Type
            };
            return dbAddress;
        }
    }
}
