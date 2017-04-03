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
        public void DbItem_ToDto_Should_BeCorrect()
        {
            var dtoItem = _dbItem.ToDto();

            dtoItem.ISBN.Should().Be(_dbItem.ISBN);
            dtoItem.Author.Should().Be(_dbItem.Author);
            dtoItem.Title.Should().Be(_dbItem.Title);
            dtoItem.Description.Should().Be(_dbItem.Description);
            dtoItem.IsLendable.Should().Be(_dbItem.IsLendable);

            dtoItem.SubjectArea.Id.Should().Be(_dbItem.SubjectArea.Id);
            dtoItem.SubjectArea.Name.Should().Be(_dbItem.SubjectArea.Name);
        }

        [Fact]
        public void DtoItem_ToDb_Should_BeCorrect()
        {
            var dbItem = _itemDto.ToDb();

            dbItem.ISBN.Should().Be(_itemDto.ISBN);                               
            dbItem.Author.Should().Be(_itemDto.Author);
            dbItem.Title.Should().Be(_itemDto.Title);
            dbItem.Description.Should().Be(_itemDto.Description);
            dbItem.IsLendable.Should().Be(_itemDto.IsLendable);

            dbItem.SubjectArea.Id.Should().Be(_itemDto.SubjectArea.Id);
            dbItem.SubjectArea.Name.Should().Be(_itemDto.SubjectArea.Name);
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
