using System;
using System.Collections.Generic;
using System.Text;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.Models
{
    public class DbAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public EDbAddressType Type { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
