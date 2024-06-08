using ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Persistence.Contracts
{
    public interface INotificationRepository: IGenericRepository<NotificationEntity>
    {
        public Task<List<NotificationEntity>> GetNotificationByRecieverId(Guid userId);
        public Task MarkNotificationsAsRead(List<Guid> notificationIds);
    }
}
