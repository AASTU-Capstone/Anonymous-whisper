using ComplaintSystem.Application.Responses;
using MediatR;

namespace ComplaintSystem.Application.Features.Notifications.Request.Queries
{
    public class GetUnreadNotificationsRequest : IRequest<BaseResponseClass>
    {
        public Guid UserId { get; set; }
    }
}