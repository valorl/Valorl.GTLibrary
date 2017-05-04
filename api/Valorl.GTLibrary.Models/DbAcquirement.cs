using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.Models
{
    public class DbAcquirement
    {
        public Guid Id { get; set; }
        public DateTime AcquirementDateUtc { get; set; }
        public EDbAcquirementStatus Status { get; set; }
        public string ItemIsbn{ get; set; }
        public Guid ReceivingLibraryId { get; set; }
        public Guid GivingLibraryId { get; set; }

        public int[] CopyNumbers { get; set; }
    }
}
