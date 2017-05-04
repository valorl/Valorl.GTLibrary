using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Valorl.GTLibrary.DTOs.Enums;

namespace Valorl.GTLibrary.DTOs
{
    public class AddressDto
    {
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        [MinLength(1)]
        public string Number { get; set; }
        [Required]
        public EAddressTypeDto Type { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
    }
}
