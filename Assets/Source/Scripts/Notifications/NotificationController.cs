using Notifications;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class NotificationController : INotificationsController
{
    protected string _channelID;
    protected string _iconPath;
    protected List<Notification> _notifications;

    public NotificationController(List<Notification> notifications, string iconPath, string channelID) : this(iconPath, channelID)
    {
        _notifications = notifications;
    }

    public NotificationController(string iconPath, string channelID)
    {
        _iconPath = iconPath;
        _channelID = channelID;
        _notifications = new List<Notification>();
    }

    public abstract void CancelNotification(int id);

    public abstract Task RequestPermission();
    public abstract int SendNotification(Notification notification);

    public void CancelNotifications()
    {
        for (int i = 0; i < _notifications.Count; i++)
        {
            CancelNotification(_notifications[i].NotificationData.id);
        }
    }

    public void SendNotifications()
    {
        for (int i = 0; i < _notifications.Count; i++)
        {
            int id = SendNotification(_notifications[i]);
            _notifications[i].SetID(id);
        }
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }
}
