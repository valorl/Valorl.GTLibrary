using System;
using System.Collections.Generic;
using System.Text;
using Valorl.GTLibrary.DTOs.Enums;

namespace Valorl.GTLibrary.DTOs
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public EAddressTypeDto Type { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
