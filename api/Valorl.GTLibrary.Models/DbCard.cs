using System;
using System.Collections.Generic;
using System.Text;

namespace Valorl.GTLibrary.Models
{
    public class DbCard
    {
        public Guid Id { get; set; }
        public Uri PictureUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
