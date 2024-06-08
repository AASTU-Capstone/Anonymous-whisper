using AutoMapper;
using ComplaintSystem.Application.DTOs.NotificationDto;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Notifications.Request.Commands
{
    public class CreateNotificationCommand: IRequest<BaseResponseClass>
    {
        public CreateNotificationDto CreateNotificationDto { get; set; }
    }
}
