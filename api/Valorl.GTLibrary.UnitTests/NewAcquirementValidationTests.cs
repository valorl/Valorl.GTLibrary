using System;
using System.Collections.Generic;
using System.Text;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.UnitTests.Utils;
using Xunit;

namespace Valorl.GTLibrary.UnitTests
{
    public class NewAcquirementValidationTests : ModelValidationTestBase
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void NewAcquirementDto_ItemCopyNumbers_Length_Should_BeValid(int length)
        {
            // Arrange
            var acquirement = GenerateNewAcquirementDto(length);
            // Act
            var isValid = ValidateProperty<NewAcquirementDto>(
                            nameof(acquirement.ItemCopyNumbers), 
                            acquirement.ItemCopyNumbers
                           );
            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(0)]
        public void NewAcquirementDto_ItemCopyNumbers_Length_Should_BeInvalid(int length)
        {
            // Arrange
            var acquirement = GenerateNewAcquirementDto(length);
            // Act
            var isValid = ValidateProperty<NewAcquirementDto>(
                            nameof(acquirement.ItemCopyNumbers),
                            acquirement.ItemCopyNumbers
                          );
            // Assert
            Assert.False(isValid);
        }

    }
}
