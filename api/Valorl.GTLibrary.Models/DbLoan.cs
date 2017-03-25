using System;
using System.Collections.Generic;
using System.Text;

namespace Valorl.GTLibrary.Models
{
    public class DbLoan
    {
        public Guid Id { get; set; }
        public DateTime LentAt { get; set; }
        public DateTime ReturnedAt { get; set; }
        public DateTime NotificationSentAt { get; set; }
        public DbMember Member { get; set; }
        public DbItemCopy ItemCopy { get; set; }
    }
}
