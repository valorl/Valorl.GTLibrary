using FluentAssertions;
using System;
using Valorl.GTLibrary.Api.Mappings;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.DTOs.Enums;
using Valorl.GTLibrary.Models;
using Valorl.GTLibrary.Models.Enums;
using Xunit;

namespace Valorl.GTLibrary.UnitTests
{
    public class MappingTests
    {

        [Fact]
        public void DbItem_ToDto_Should_HaveCorrectProperties()
        {
            // Arrange 
            var dbItem = CreateTestDbItem();
            // Act
            var itemDto = dbItem.ToDto();
            // Assert
            Assert.Equal(dbItem.ISBN, itemDto.ISBN);
            Assert.Equal(dbItem.Author, itemDto.Author);
            Assert.Equal(dbItem.Title, itemDto.Title);
            Assert.Equal(dbItem.Description, itemDto.Description);
            Assert.Equal(dbItem.IsLendable, itemDto.IsLendable);

            Assert.Equal(dbItem.SubjectArea.Id, itemDto.SubjectArea.Id);
            Assert.Equal(dbItem.SubjectArea.Name, itemDto.SubjectArea.Name);
        }

        [Fact]
        public void ItemDto_ToDbModel_Should_HaveCorrectProperties()
        {
            // Arrange
            var itemDto = CreateTestItemDto();
            // Act
            var dbItem = itemDto.ToDbModel();
            // Assert
            Assert.Equal(itemDto.ISBN, dbItem.ISBN);
            Assert.Equal(itemDto.Author, dbItem.Author);
            Assert.Equal(itemDto.Title, dbItem.Title);
            Assert.Equal(itemDto.Description, dbItem.Description);
            Assert.Equal(itemDto.IsLendable, dbItem.IsLendable);

            Assert.Equal(itemDto.SubjectArea.Id, dbItem.SubjectArea.Id);
            Assert.Equal(itemDto.SubjectArea.Name, dbItem.SubjectArea.Name);
        }

        [Fact]
        public void DbItemCopy_ToDto_Should_HaveCorrectProperties()
        {
            // Arrange 
            var dbItemCopy = CreateTestDbItemCopy();
            // Act
            var itemCopyDto = dbItemCopy.ToDto();
            // Assert
            Assert.Equal(dbItemCopy.Number, itemCopyDto.Number);
            Assert.Equal(dbItemCopy.ISBN, itemCopyDto.ISBN);
            Assert.Equal(dbItemCopy.IsAvailable, itemCopyDto.IsAvailable);
            Assert.Equal(EItemCopyTypeDto.Normal, itemCopyDto.Type);
        }

        [Fact]
        public void ItemCopyDto_ToDbModel_Should_HaveCorrectProperties()
        {
            // Arrange
            var itemCopyDto = CreateTestItemCopyDto();
            // Act
            var dbItemCopy = itemCopyDto.ToDbModel();
            // Assert
            Assert.Equal(itemCopyDto.Number, dbItemCopy.Number);
            Assert.Equal(itemCopyDto.ISBN, dbItemCopy.ISBN);
            Assert.Equal(itemCopyDto.IsAvailable, dbItemCopy.IsAvailable);
            Assert.Equal(EDbItemCopyType.Normal, dbItemCopy.Type);
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

        private static ItemDto CreateTestItemDto()
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

        private static DbItemCopy CreateTestDbItemCopy()
        {
            return new DbItemCopy()
            {
                Number = 1,
                ISBN = "9783161484100",
                IsAvailable = true,
                Type = EDbItemCopyType.Normal
            };
        }

        private static ItemCopyDto CreateTestItemCopyDto()
        {
            return new ItemCopyDto()
            {
                Number = 1,
                ISBN = "9783161484100",
                IsAvailable = true,
                Type = EItemCopyTypeDto.Normal
            };
        }

    }
}
