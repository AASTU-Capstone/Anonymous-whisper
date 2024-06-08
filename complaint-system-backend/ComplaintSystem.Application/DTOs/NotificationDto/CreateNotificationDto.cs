using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.NotificationDto
{
    public class CreateNotificationDto
    {
        public string? Sender { get; set; }
        public string? Message { get; set; }
        public Guid RecieverId { get; set; }
    }
}
