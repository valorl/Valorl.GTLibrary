using System;
using System.Collections.Generic;
using System.Text;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.DTOs.Enums;
using Valorl.GTLibrary.Models;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.UnitTests.Utils
{
    public class MappingTestBase
    {
        public static DbItem CreateTestDbItem()
        {
            return new DbItem()
            {
                ISBN = "9783161484100",
                Author = "TestAuthor",
                Title = "TestTitle",
                Description = "TestDescription",
                IsLendable = true,
                SubjectArea = new DbSubjectArea()
                {
                    Id = 123,
                    Name = "TestSubjectArea"
                }
            };
        }

        public static ItemDto CreateTestItemDto()
        {
            return new ItemDto()
            {
                ISBN = "9783161484100",
                Author = "TestAuthor",
                Title = "TestTitle",
                Description = "TestDescription",
                IsLendable = true,
                SubjectArea = new SubjectAreaDto()
                {
                    Id = 123,
                    Name = "TestSubjectArea"
                }
            };
        }

        public static DbItemCopy CreateTestDbItemCopy()
        {
            return new DbItemCopy()
            {
                Number = 1,
                ISBN = "9783161484100",
                IsAvailable = true,
                Type = EDbItemCopyType.Normal
            };
        }

        public static ItemCopyDto CreateTestItemCopyDto()
        {
            return new ItemCopyDto()
            {
                Number = 1,
                ISBN = "9783161484100",
                IsAvailable = true,
                Type = EItemCopyTypeDto.Normal
            };
        }

        public static DbAddress CreateTestDbAddress()
        {
            return new DbAddress()
            {
                Id = 1,
                Street = "TestStreet",
                Number = "123.XYZ",
                Type = EDbAddressType.Library,
                Country = "TestCountry",
                ZipCode = "1234",
                City = "TestCity"
            };
        }

        public static AddressDto CreateTestAddressDto()
        {
            throw new NotImplementedException();
        }
    }
}
