using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Valorl.GTLibrary.DTOs
{
    public class NewAcquirementDto
    {
        [Required]
        public Guid LibraryId { get; set; }
        [Required]
        public string ItemIsbn { get; set; }
        [Required]
        [MinLength(1)]
        public int[] ItemCopyNumbers { get; set; }
    }
}
