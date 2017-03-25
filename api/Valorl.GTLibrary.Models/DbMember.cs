using System;
using System.Collections.Generic;
using System.Text;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.Models
{
    public class DbMember
    {
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public EMemberType Type { get; set; }
        public string CampusAddress { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNr { get; set; }
        public string Email { get; set; }
    }
}
