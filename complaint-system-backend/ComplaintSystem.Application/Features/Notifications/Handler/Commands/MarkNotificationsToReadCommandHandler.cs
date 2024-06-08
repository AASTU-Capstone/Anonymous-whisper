using ComplaintSystem.Application.Features.Notifications.Request.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;

namespace ComplaintSystem.Application.Features.Notifications.Handler.Commands
{
    public class MarkNotificationsToReadCommandHandler : IRequestHandler<MarkNotificationsToReadCommand, BaseResponseClass>
    {
        private readonly INotificationRepository _notificationRepository;

        public MarkNotificationsToReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<BaseResponseClass> Handle(MarkNotificationsToReadCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseClass();
            await _notificationRepository.MarkNotificationsAsRead(request.NotificationIds);

            response.Message = "Notifications Marked as Read Successfully";
            response.Success = true;
            response.StatusCode = 204;

            return response;
        }
    }
}