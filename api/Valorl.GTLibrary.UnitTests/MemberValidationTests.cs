using System;
using System.Collections.Generic;
using System.Text;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.UnitTests.Utils;
using Xunit;

namespace Valorl.GTLibrary.UnitTests
{
    public class MemberValidationTests : ModelValidationTestBase
    {
        [Fact]
        public void MemberDto_SSN_Length9_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto {SSN = "123456789"};
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.SSN), member.SSN);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_SSN_Length8_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { SSN = "12345678" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.SSN), member.SSN);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_SSN_Length10_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { SSN = "1234567890" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.SSN), member.SSN);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_SSN_ContainsCharacter_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { SSN = "12345678a" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.SSN), member.SSN);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_SSN_Empty_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { SSN = "" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.SSN), member.SSN);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_FirstName_Length1_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { FirstName = "A" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.FirstName), member.FirstName);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_FirstName_LengthMoreThan1_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { FirstName = "Edmond" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.FirstName), member.FirstName);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_FirstName_ContainsApostrophe_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { FirstName = "J'Marcus" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.FirstName), member.FirstName);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_FirstName_ContainsNumber_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { FirstName = "Jos3ph" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.FirstName), member.FirstName);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_FirstName_ContainsSpace_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { FirstName = "George Michael" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.FirstName), member.FirstName);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_FirstName_Empty_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { FirstName = "" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.FirstName), member.FirstName);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_MiddleName_Empty_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { MiddleName = "" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.MiddleName), member.MiddleName);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_MiddleName_ContainsApostrophe_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { MiddleName = "J'Marcus" };
            // Act
                var isValid = ValidateProperty<MemberDto>(nameof(member.MiddleName), member.MiddleName);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_MiddleName_ContainsNumber_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { MiddleName = "Jos3ph" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.MiddleName), member.MiddleName);
            // Assert
            Assert.False(isValid);
        }
        [Fact]
        public void MemberDto_MiddleName_ContainsSpace_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { MiddleName = "George Michael" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.MiddleName), member.MiddleName);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_LastName_Length1_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { LastName = "A" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.LastName), member.LastName);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_LastName_Length5_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { LastName = "Smith" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.LastName), member.LastName);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_LastName_ContainsSpace_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { LastName = "di Maria" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.LastName), member.LastName);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_LastName_ContainsApostrophe_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { LastName = "d'Arthur" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.LastName), member.LastName);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_LastName_ContainsNumber_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { LastName = "Jos3ph" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.LastName), member.LastName);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_LastName_Empty_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { LastName = "" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.LastName), member.LastName);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_PhoneNr_Length6_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { PhoneNr = "123456" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.PhoneNr), member.PhoneNr);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_PhoneNr_Length4_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { PhoneNr = "12345678901234" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.PhoneNr), member.PhoneNr);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_PhoneNr_Length5_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { PhoneNr = "12345" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.PhoneNr), member.PhoneNr);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_PhoneNr_Length15_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { PhoneNr = "123456789012345" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.PhoneNr), member.PhoneNr);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_PhoneNr_ContainsPlus_ShouldBe_Valid()

        {
            // Arrange
            var member = new MemberDto { PhoneNr = "+123456" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.PhoneNr), member.PhoneNr);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_PhoneNr_ContainsSpaces_ShouldBe_Invalid()

        {
            // Arrange
            var member = new MemberDto { PhoneNr = "12 34 56 78" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.PhoneNr), member.PhoneNr);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_PhoneNr_ContainsDashes_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { PhoneNr = "12-34-56-78" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.PhoneNr), member.PhoneNr);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_PhoneNr_ContainsDots_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { PhoneNr = "12.34.56.78" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.PhoneNr), member.PhoneNr);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_PhoneNr_Empty_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { PhoneNr = "" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.PhoneNr), member.PhoneNr);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_Email_UsernameAtDomainDotTld_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { Email = "testmail@gmail.com" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.Email), member.Email);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_Email_NoAtNoDomainNoTld_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { Email = "testmail" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.Email), member.Email);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_Email_Empty_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { Email = "" };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.Email), member.Email);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_Addresses_Empty_ShouldBe_Invalid()
        {
            // Arrange
            var member = new MemberDto { Addresses = new List<AddressDto>() };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.Addresses), member.Addresses);
            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void MemberDto_Addresses_One_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto { Addresses = new List<AddressDto>() { new AddressDto() } };
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.Addresses), member.Addresses);
            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MemberDto_Addresses_Two_ShouldBe_Valid()
        {
            // Arrange
            var member = new MemberDto {Addresses = new List<AddressDto>() {new AddressDto(), new AddressDto()}};
            // Act
            var isValid = ValidateProperty<MemberDto>(nameof(member.Addresses), member.Addresses);
            // Assert
            Assert.True(isValid);
        }
    }
}
