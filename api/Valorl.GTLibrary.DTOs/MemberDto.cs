using System;
using System.Collections.Generic;
using System.Text;
using Valorl.GTLibrary.DTOs.Enums;

namespace Valorl.GTLibrary.DTOs
{
    public class MemberDto
    {
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public EMemberTypeDto Type { get; set; }
        public string PhoneNr { get; set; }
        public string Email { get; set; }

        public IEnumerable<AddressDto> Addresses { get; set; }
    }
}
