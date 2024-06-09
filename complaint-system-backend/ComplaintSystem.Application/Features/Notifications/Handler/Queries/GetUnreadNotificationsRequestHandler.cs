using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComplaintSystem.Application.Features.Notifications.Request.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;

namespace ComplaintSystem.Application.Features.Notifications.Handler.Queries
{
    public class GetUnreadNotificationsRequestHandler : IRequestHandler<GetUnreadNotificationsRequest, BaseResponseClass>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public GetUnreadNotificationsRequestHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseClass> Handle(GetUnreadNotificationsRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseClass();
            var notifications = await _notificationRepository.GetNotificationByRecieverId(request.UserId);

            if (notifications.Count == 0)
            {
                response.Message = "No Unread Notifications Found";
                response.Success = true;
                response.StatusCode = 200;
            }

            else
            {
                response.Data = _mapper.Map<List<NotificationEntity>>(notifications);
                response.Message = "Unread Notifications Fetched Successfully";
                response.Success = true;
                response.StatusCode = 200;
            }

            return response;
        }
    }
}