using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.DTOs.Enums;

namespace Valorl.GTLibrary.UnitTests.Utils
{
    public class ModelValidationTestBase
    {
        protected bool ValidateInstance(object model)
            => Validator.TryValidateObject(model, new ValidationContext(model, null, null), null, true);

        protected bool ValidateProperty<T>(string name, object value) where T : new()
        {
            var results = new List<ValidationResult>();
            return Validator.TryValidateProperty(value,
                    new ValidationContext(new T(), null, null) { MemberName = name },
                    results);
        }

        protected MemberDto GenerateValidMember()
        {
            return new MemberDto()
            {
                SSN = "123456789",
                FirstName = "Gandalf",
                LastName = "The White",
                Type = EMemberTypeDto.Student,
                PhoneNr = "+4552112242",
                Email = "gandalf.white@gmail.com",
                Addresses = new [] { new AddressDto(), new AddressDto() }
            }; 
        }

        protected NewAcquirementDto GenerateNewAcquirementDto(int noOfCopies = 1)
        {
            return new NewAcquirementDto()
            {
                ItemIsbn = "9871234567890",
                LibraryId = Guid.Empty,
                ItemCopyNumbers = Enumerable.Range(1, noOfCopies).ToArray()
            };
        }
    }
}
