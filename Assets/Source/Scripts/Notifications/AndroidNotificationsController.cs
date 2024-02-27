using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

namespace Notifications
{
    public class AndroidNotificationsController : NotificationController
    {
        public AndroidNotificationsController(List<Notification> notifications, string iconPath, string channelID) : base(notifications, iconPath, channelID)
        {

        }

        public AndroidNotificationsController(string iconPath, string channelID) : base(iconPath, channelID)
        {
            var channel = new AndroidNotificationChannel()
            {
                Name = channelID,
                Id = _channelID,
                Description = "Generic",
                Importance = Importance.High,
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }

        public override int SendNotification(Notification notification)
        {
            var notificationData = notification.NotificationData;
            AndroidNotification androidNotification = new AndroidNotification()
            {
                Title = notificationData.title,
                Text = notificationData.text,
                LargeIcon = _iconPath,

                FireTime = DateTime.Now.AddSeconds(notification.GetDelayToShow())
            };
            Debug.Log($"Send: {androidNotification.Text} {androidNotification.FireTime}");
            return AndroidNotificationCenter.SendNotification(androidNotification, _channelID);
        }

        public override void CancelNotification(int id)
        {
            AndroidNotificationCenter.CancelNotification(id);
        }

        public override async Task RequestPermission()
        {
            if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
            {
                Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
            }
        }

        public override void CancelNotifications()
        {
            AndroidNotificationCenter.CancelAllNotifications();
        }
    }
}