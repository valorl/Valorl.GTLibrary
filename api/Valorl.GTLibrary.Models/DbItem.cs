using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Valorl.GTLibrary.Models
{
    public class DbItem
    {
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsLendable { get; set; }
        public DbSubjectArea SubjectArea { get; set; }
    }
}
