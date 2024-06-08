using ComplaintSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Domain.Entities
{
    public class NotificationEntity : BaseEntity
    {
        public string Sender {get; set;}
        public string Message { get; set; }
        public Guid ReceiverId { get; set; }
        public DateTime Date { get; set; }
    }
}
