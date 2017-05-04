using System;
using GenFu;
using Valorl.GTLibrary.Api.Mappings;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.DTOs.Enums;
using Valorl.GTLibrary.Models;
using Valorl.GTLibrary.Models.Enums;
using Valorl.GTLibrary.UnitTests.Utils;
using Xunit;

namespace Valorl.GTLibrary.UnitTests
{
    public class MappingTests : MappingTestBase
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

            //AssertUtils.AggregateMultiple(
            //    () => Assert.Equal(1, 2),
            //    () => Assert.Equal(3, 4));
        }

        [Fact]
        public void DbAddress_ToDto_Should_HaveCorrectProperties()
        {
            // Arrange
            var dbAddress = A.New<DbAddress>();
            // Act
            var dto = dbAddress.ToDto();
            // Assert
            AssertUtils.AggregateMultiple(
                () => Assert.Equal(dbAddress.Id, dto.Id),
                () => Assert.Equal(dbAddress.Street, dto.Street),
                () => Assert.Equal(dbAddress.Number, dto.Number),
                () => Assert.Equal(dbAddress.Type, (EDbAddressType)(int)dto.Type),
                () => Assert.Equal(dbAddress.Country, dto.Country),
                () => Assert.Equal(dbAddress.ZipCode, dto.ZipCode),
                () => Assert.Equal(dbAddress.City, dto.City)
            );
        }

        [Fact]
        public void AddressDto_ToDbModel_Should_HaveCorrectProperties()
        {
            // Arrange
            var dto = A.New<AddressDto>();
            // Act
            var dbAddress = dto.ToDbModel();
            // Assert
            AssertUtils.AggregateMultiple(
                () => Assert.Equal(dto.Id, dbAddress.Id),
                () => Assert.Equal(dto.Street, dbAddress.Street),
                () => Assert.Equal(dto.Number, dbAddress.Number),
                () => Assert.Equal(dto.Type, (EAddressTypeDto)(int)dbAddress.Type),
                () => Assert.Equal(dto.Country, dbAddress.Country),
                () => Assert.Equal(dto.ZipCode, dbAddress.ZipCode),
                () => Assert.Equal(dto.City, dbAddress.City)
            );
        }

        [Fact]
        public void DbLibrary_ToDto_Should_HaveCorrectProperties()
        {
            // Arrange
            var dbLibrary = A.New<DbLibrary>();
            dbLibrary.Address = A.New<DbAddress>();
            // Act
            var dto = dbLibrary.ToDto();
            // Assert
            AssertUtils.AggregateMultiple(
                () => Assert.Equal(dbLibrary.Id, dto.Id),
                () => Assert.Equal(dbLibrary.Name, dto.Name)
            );
        }
    }
}
