using System;

namespace Valorl.GTLibrary.DTOs
{
    public class ItemDto
    {
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsLendable { get; set; }
        public SubjectAreaDto SubjectArea { get; set; }
    }
}
