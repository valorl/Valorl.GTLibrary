using System;
using System.Collections.Generic;
using System.Text;
using Valorl.GTLibrary.DTOs.Enums;

namespace Valorl.GTLibrary.DTOs
{
    public class AcquirementDto
    {
        public Guid Id { get; set; }
        public DateTime DateUtc { get; set; }
        public EAcquirementStatusDto Status { get; set; }
        public ItemDto Item { get; set; }
        public LibraryDto Receiver { get; set; }
        public LibraryDto Sender { get; set; }

        public IEnumerable<ItemCopyDto> ItemCopies { get; set; }
    }
}
