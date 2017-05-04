using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Valorl.GTLibrary.DTOs.Enums;

namespace Valorl.GTLibrary.DTOs
{
    public class MemberDto
    {
        [Required]
        [RegularExpression(@"^\d{9}$")]
        public string SSN { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z']+$")] // 1+ letter or ' no spaces
        public string FirstName { get; set; }
        [RegularExpression(@"^[A-Za-z']*$")] // 1+ letter or ' no spaces
        public string MiddleName { get; set; } // 0+ letter or '
        [Required]
        [RegularExpression(@"^[A-Za-z' ]+$")] // 1+ letter or ' or space
        public string LastName { get; set; }
        [Required]
        public EMemberTypeDto Type { get; set; }
        [Required]
        [RegularExpression(@"^[+]{0,1}[0-9]{6,14}$")]
        public string PhoneNr { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<AddressDto> Addresses { get; set; }
    }
}
