using System.Threading.Tasks;

namespace Notifications
{
    public interface INotificationsController
    {
        void AddNotification(Notification notification);
        void CancelNotification(int id);
        void CancelNotifications();
        int SendNotification(Notification notification);
        void SendNotifications();
        Task RequestPermission();
    }
}