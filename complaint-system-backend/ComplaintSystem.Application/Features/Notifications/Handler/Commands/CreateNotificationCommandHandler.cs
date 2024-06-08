using AutoMapper;
using ComplaintSystem.Application.Features.Managers.Requests.Commands;
using ComplaintSystem.Application.Features.Notifications.Request.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Notifications.Handler.Commands
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, BaseResponseClass>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public CreateNotificationCommandHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseClass> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseClass();
            var notification = _mapper.Map<NotificationEntity>(request.CreateNotificationDto);
            await _notificationRepository.Add(notification);

            response = new BaseResponseClass
            {
                Id = notification.Id,
                Message = "Notification Created Successfully",
                Success = true,
                StatusCode = 201,
            };

            return response;
        }
}
