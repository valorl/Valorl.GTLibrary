using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Valorl.GTLibrary.Api.Controllers;
using Valorl.GTLibrary.DataAccess.Interfaces;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.UnitTests.Utils;
using Xunit;

namespace Valorl.GTLibrary.UnitTests
{
    public class AcquirementControllerTests
    {
        private readonly AcquirementsController _controller;
        private readonly Mock<IAcquirementRepository> _acqRepoMock = MockRepositories.Acquirements.GetRepository();
        private readonly Mock<IItemRepository> _itemRepoMock = MockRepositories.Items.GetRepository();
        private readonly Mock<IItemCopyRepository> _itemCopyRepoMock = MockRepositories.ItemCopies.GetRepository();
        private readonly Mock<ILibraryRepository> _libraryRepoMock = MockRepositories.Libraries.GetRepository();

        public AcquirementControllerTests()
        {
            var acqRepo = _acqRepoMock.Object;
            var itemRepo = _itemRepoMock.Object;
            var itemCopyRepo = _itemCopyRepoMock.Object;
            var libraryRepo = _libraryRepoMock.Object;
            _controller = new AcquirementsController(acqRepo, libraryRepo, itemRepo, itemCopyRepo);
        }

        [Fact]
        public void PostAcquirement_Should_ReturnCreated()
        {
            // Arrange
            var dto = new NewAcquirementDto()
            {
                ItemIsbn = MockRepositories.Items.AnExistingIsbn,
                ItemCopyNumbers = new[] { MockRepositories.ItemCopies.AnAvailableCopyNr },
                LibraryId = MockRepositories.Libraries.AnExistingLibraryId
            };
            // Act
            var result = _controller.PostAcquirement(dto).Result;
            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.IsType<AcquirementDto>(createdResult.Value);
        }

        [Fact]
        public void PostAcquirement_Given_UnavailableCopy_Should_ReturnBadRequest()
        {
            // Arrange
            var dto = new NewAcquirementDto()
            {
                ItemIsbn = MockRepositories.Items.AnExistingIsbn,
                ItemCopyNumbers = new [] { MockRepositories.ItemCopies.AnUnavailableCopyNr },
                LibraryId = MockRepositories.Libraries.AnExistingLibraryId
            };
            // Act
            var result = _controller.PostAcquirement(dto).Result;
            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PostAcquirement_Given_NoItem_Should_ReturnBadRequest()
        {
            // Arrange
            var dto = new NewAcquirementDto()
            {
                ItemIsbn = MockRepositories.Items.ANonExistingIsbn,
                ItemCopyNumbers = new[] { MockRepositories.ItemCopies.AnAvailableCopyNr},
                LibraryId = MockRepositories.Libraries.AnExistingLibraryId
            };
            // Act
            var result = _controller.PostAcquirement(dto).Result;
            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PostAcquirement_Given_NoItemCopy_Should_ReturnBadRequest()
        {
            // Arrange
            var dto = new NewAcquirementDto()
            {
                ItemIsbn = MockRepositories.Items.AnExistingIsbn,
                ItemCopyNumbers = new[]
                {
                    MockRepositories.ItemCopies.AnAvailableCopyNr,
                    MockRepositories.ItemCopies.ANonExistingCopyNr
                },
                LibraryId = MockRepositories.Libraries.AnExistingLibraryId
            };
            // Act
            var result = _controller.PostAcquirement(dto).Result;
            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PostAcquirement_Given_NoLibrary_Should_ReturnBadRequest()
        {
            // Arrange
            var dto = new NewAcquirementDto()
            {
                ItemIsbn = MockRepositories.Items.AnExistingIsbn,
                ItemCopyNumbers = new[] { MockRepositories.ItemCopies.AnAvailableCopyNr },
                LibraryId = MockRepositories.Libraries.ANonExistingLibraryId
            };
            // Act
            var result = _controller.PostAcquirement(dto).Result;
            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PostAcquirement_Should_FetchEachItemCopy()
        {
            // Arrange
            var dto = new NewAcquirementDto()
            {
                ItemIsbn = MockRepositories.Items.AnExistingIsbn,
                ItemCopyNumbers = new[]
                {
                    MockRepositories.ItemCopies.AnAvailableCopyNr,
                    MockRepositories.ItemCopies.AnUnavailableCopyNr
                },
                LibraryId = MockRepositories.Libraries.AnExistingLibraryId
            };
            // Act
            _controller.PostAcquirement(dto).Wait();
            // Assert
            foreach (var copyNr in dto.ItemCopyNumbers)
            {
                _itemCopyRepoMock.Verify(repo => repo.GetOne(copyNr, dto.ItemIsbn), Times.Once);
            }
        }

        [Fact]
        public void PostAcquirement_Should_FetchItem_Once()
        {
            // Arrange
            var dto = new NewAcquirementDto()
            {
                ItemIsbn = MockRepositories.Items.AnExistingIsbn,
                ItemCopyNumbers = new[] { MockRepositories.ItemCopies.AnAvailableCopyNr },
                LibraryId = MockRepositories.Libraries.AnExistingLibraryId
            };
            // Act
            _controller.PostAcquirement(dto).Wait();
            // Assert
            _itemRepoMock.Verify(repo => repo.GetOneByIsbn(MockRepositories.Items.AnExistingIsbn), Times.Once);
        }

        [Fact]
        public void PostAcquirement_Should_FetchReceiverLibrary_Once()
        {
            // Arrange
            var dto = new NewAcquirementDto()
            {
                ItemIsbn = MockRepositories.Items.AnExistingIsbn,
                ItemCopyNumbers = new[] { MockRepositories.ItemCopies.AnAvailableCopyNr },
                LibraryId = MockRepositories.Libraries.AnExistingLibraryId
            };
            // Act
            _controller.PostAcquirement(dto).Wait();
            // Assert
            _libraryRepoMock.Verify(repo => repo.GetOne(MockRepositories.Libraries.AnExistingLibraryId), Times.Once);
        }

        [Fact]
        public void PostAcquirement_Should_FetchThisLibrary_Once()
        {
            // Arrange
            var dto = new NewAcquirementDto()
            {
                ItemIsbn = MockRepositories.Items.AnExistingIsbn,
                ItemCopyNumbers = new[] { MockRepositories.ItemCopies.AnAvailableCopyNr },
                LibraryId = MockRepositories.Libraries.AnExistingLibraryId
            };
            // Act
            _controller.PostAcquirement(dto).Wait();
            // Assert
            _libraryRepoMock.Verify(repo => repo.GetOne(MockRepositories.Libraries.ThisLibraryId), Times.Once);
        }

        [Fact]
        public void GetAcquirement_Given_AllGood_Should_ReturnOkDto()
        {
            // Act
            var result = _controller.GetAcquirement(MockRepositories.Acquirements.AllGoodAcquirementId).Result;
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<AcquirementDto>(okResult.Value);
            Assert.Equal(MockRepositories.Acquirements.AllGoodAcquirementId, dto.Id);
        }

        [Fact]
        public void GetAcquirement_Given_ItemMissing_Should_ReturnInternalServerError()
        {
            // Act
            var result = _controller.GetAcquirement(MockRepositories.Acquirements.AcquirementIdWithMissingItem).Result;
            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errorResult.StatusCode);
        }

        [Fact]
        public void GetAcquirement_Given_ItemCopyMissing_Should_ReturnInternalServerError()
        {
            // Act
            var result = _controller.GetAcquirement(MockRepositories.Acquirements.AcquirementIdWithMissingCopy).Result;
            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errorResult.StatusCode);
        }

        [Fact]
        public void GetAcquirement_Given_SenderLibraryMissing_Should_ReturnInternalServerError()
        {
            // Act
            var result = _controller.GetAcquirement(MockRepositories.Acquirements.AcquirementIdWithMissingSender).Result;
            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errorResult.StatusCode);
        }

        [Fact]
        public void GetAcquirement_Given_ReceiverLibraryMissing_Should_ReturnInternalServerError()
        {
            // Act
            var result = _controller.GetAcquirement(MockRepositories.Acquirements.AcquirementIdWithMissingReceiver).Result;
            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errorResult.StatusCode);
        }

    }
}
