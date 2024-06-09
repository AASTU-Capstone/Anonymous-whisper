using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintSystem.Application.Responses;
using MediatR;

namespace ComplaintSystem.Application.Features.Notifications.Request.Commands
{
    public class MarkNotificationsToReadCommand : IRequest<BaseResponseClass>
    {
        public List<string> NotificationIds { get; set; }
    }
}