using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComplaintSystem.Persistence.Repositories
{
    public class NotificationRepository : GenericRepository<NotificationEntity>, INotificationRepository
    {
        private readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;

        public NotificationRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }
        public async Task<List<NotificationEntity>> GetNotificationByRecieverId(Guid userId)
        {
            var notification = await _complaintSystemAppDbContext.Notifications
                .Where(c => c.recieverId == userId && c.isRead == false)
                .OrderByDescending(c => c.createdAt)
                .ToListAsync();

            return notification;
        }

        public async Task MarkNotificationsAsRead(List<string> notificationIds)
        {
            foreach (var notificationId in notificationIds)
            {
                var Nid = new Guid(notificationId);
                var notification = await _complaintSystemAppDbContext.Notifications.FirstOrDefaultAsync(c => c.id == Nid);

                if (notification != null)
                {
                    notification.isRead = true;
                }
            }

            await _complaintSystemAppDbContext.SaveChangesAsync();
        }
    }
}
