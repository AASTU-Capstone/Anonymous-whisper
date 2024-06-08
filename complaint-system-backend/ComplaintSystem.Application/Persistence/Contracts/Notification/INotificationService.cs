using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Application.Persistence.Contracts.Notification
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string userId, NotificationEntity notification);
    }
}