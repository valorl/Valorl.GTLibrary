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
        private readonly DtoItem _dtoItem;
        public MappingTests()
        {
            _dbItem = CreateTestDbItem();
            _dtoItem = CreateTestDtoItem();
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
            var dbItem = _dtoItem.ToDb();

            dbItem.ISBN.Should().Be(_dtoItem.ISBN);                               
            dbItem.Author.Should().Be(_dtoItem.Author);
            dbItem.Title.Should().Be(_dtoItem.Title);
            dbItem.Description.Should().Be(_dtoItem.Description);
            dbItem.IsLendable.Should().Be(_dtoItem.IsLendable);

            dbItem.SubjectArea.Id.Should().Be(_dtoItem.SubjectArea.Id);
            dbItem.SubjectArea.Name.Should().Be(_dtoItem.SubjectArea.Name);
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

        private static DtoItem CreateTestDtoItem()
        {
            return new DtoItem()
            {
                ISBN = "9783161484100",
                Author = "TestAuthor",
                Title = "TestTitle",
                Description = "TestDescription",
                IsLendable = true,
                SubjectArea = new DtoSubjectArea()
                {
                    Id = 123,
                    Name = "TestSubjectArea"
                }
            };
        }
    }
}
