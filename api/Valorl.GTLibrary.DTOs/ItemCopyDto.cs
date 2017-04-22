using System;
using System.Collections.Generic;
using System.Text;
using Valorl.GTLibrary.DTOs.Enums;

namespace Valorl.GTLibrary.DTOs
{
    public class ItemCopyDto
    {
        public string ISBN { get; set; }
        public int Number { get; set; }
        public EItemCopyTypeDto Type { get; set; }
        public bool IsAvailable { get; set; }
    }
}
