using System;
using System.ComponentModel.DataAnnotations;

namespace Valorl.GTLibrary.DTOs
{
    public class ItemDto
    {
        [Required]
        [RegularExpression(@"\b98(7|9)\d{10}")]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsLendable { get; set; }

        [Required]
        public SubjectAreaDto SubjectArea { get; set; }
    }
}
