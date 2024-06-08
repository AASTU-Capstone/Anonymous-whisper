using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.NotificationDto
{
    public class GetNotificationDto
    {
        public Guid Id { get; set; }
        public string? Sender { get; set; }
        public string? Message { get; set; }
        public Guid RecieverId { get; set; }
        public bool isRead { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
