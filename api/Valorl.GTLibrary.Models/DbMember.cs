﻿using System;
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
        public EDbMemberType Type { get; set; }
        public string PhoneNr { get; set; }
        public string Email { get; set; }

        public IEnumerable<DbAddress> Addresses { get; set; }
    }
}
