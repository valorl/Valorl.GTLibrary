﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Valorl.GTLibrary.Models
{
    public class DbLibrary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DbAddress Address { get; set; }
    }
}
