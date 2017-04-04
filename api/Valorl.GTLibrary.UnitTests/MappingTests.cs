using FluentAssertions;
using System;
using Valorl.GTLibrary.Api.Mappings;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.Models;
using Xunit;

namespace Valorl.GTLibrary.UnitTests
{
    public class MappingTests
    {
        private readonly DbItem _dbItem;
        private readonly ItemDto _itemDto;
        public MappingTests()
        {
            _dbItem = CreateTestDbItem();
            _itemDto = CreateTestDtoItem();
        }

        [Fact]
        public void DbItem_ToDto_Should_BeIdentical()
        {
            // Act
            var dtoItem = _dbItem.ToDto();

            // Assert
            Assert.Equal(dtoItem.ISBN, _dbItem.ISBN);
            Assert.Equal(dtoItem.Author, _dbItem.Author);
            Assert.Equal(dtoItem.Title, _dbItem.Title);
            Assert.Equal(dtoItem.Description, _dbItem.Description);
            Assert.Equal(dtoItem.IsLendable, _dbItem.IsLendable);

            Assert.Equal(dtoItem.SubjectArea.Id, _dbItem.SubjectArea.Id);
            Assert.Equal(dtoItem.SubjectArea.Name, _dbItem.SubjectArea.Name);
        }

        [Fact]
        public void ItemDto_ToDb_Should_BeIdentical()
        {
            // Act
            var dbItem = _itemDto.ToDb();

            // Assert
            Assert.Equal(dbItem.ISBN, _itemDto.ISBN);
            Assert.Equal(dbItem.Author, _itemDto.Author);
            Assert.Equal(dbItem.Title, _itemDto.Title);
            Assert.Equal(dbItem.Description, _itemDto.Description);
            Assert.Equal(dbItem.IsLendable, _itemDto.IsLendable);

            Assert.Equal(dbItem.SubjectArea.Id, _itemDto.SubjectArea.Id);
            Assert.Equal(dbItem.SubjectArea.Name, _itemDto.SubjectArea.Name);
        }


        private static DbItem CreateTestDbItem()
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

        private static ItemDto CreateTestDtoItem()
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
    }
}
