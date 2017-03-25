using System;
using System.Collections.Generic;
using System.Text;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.Models
{
    public class DbItemCopy
    {
        public string ISBN { get; set; }
        public int Number { get; set; }
        public EItemCopyType Type { get; set; }
        public bool IsAvailable { get; set; }
    }
}
