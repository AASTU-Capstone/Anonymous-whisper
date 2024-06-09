using ComplaintSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Domain.Entities
{
    public class NotificationEntity
    {
        public Guid id { get; set; }
        public string? sender {get; set; }
        public string? message { get; set; }
        public Guid recieverId { get; set; }
        public bool isRead { get; set; } = false;
        public DateTime createdAt { get; set; }
    }
}
