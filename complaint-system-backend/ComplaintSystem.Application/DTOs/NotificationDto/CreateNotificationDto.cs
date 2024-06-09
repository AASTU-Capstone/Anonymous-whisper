using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.NotificationDto
{
    public class CreateNotificationDto
    {
        public string? sender { get; set; }
        public string? message { get; set; }
        public Guid recieverId { get; set; }
    }
}
