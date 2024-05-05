using ComplaintSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Domain.Entities
{
    public class NotificationEntity:BaseEntity
    {
        public string Message { get; set; }
        public Guid UserEntityId { get; set; }
    }
}
