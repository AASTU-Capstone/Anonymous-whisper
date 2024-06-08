using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using ComplaintSystem.Application.Persistence.Contracts.Notification;
using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Infrastructure.services
{
    public class NotificationService : INotificationService
    {
        private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public async Task SendNotificationAsync(string userId, NotificationEntity notification)
        {
            if (_sockets.TryGetValue(userId, out var socket))
            {
                var messageJson = JsonSerializer.Serialize(notification);
                var messageBytes = Encoding.UTF8.GetBytes(messageJson);
                await socket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public static void AddSocket(string userId, WebSocket socket)
        {
            _sockets[userId] = socket;
        }

        public static async Task RemoveSocket(string userId)
        {
            if (_sockets.TryRemove(userId, out var socket))
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                socket.Dispose();
            }
        }
    }
}