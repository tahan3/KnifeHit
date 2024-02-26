#if UNITY_IOS
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.iOS;
using UnityEngine;

namespace Notifications
{
    public class IOSNotificationsController : NotificationController
    {
        public IOSNotificationsController(string iconPath, string channelID) : base(iconPath, channelID)
        {
        }

        public IOSNotificationsController(List<Notification> notifications, string iconPath, string channelID) : base(notifications, iconPath, channelID)
        {
        }

        public override void CancelNotification(int id)
        {
            iOSNotificationCenter.RemoveScheduledNotification(id.ToString());
        }

        public async override void RequestPermission()
        {
            var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge;
            using (var req = new AuthorizationRequest(authorizationOption, true))
            {
                await UniTask.WaitUntil(() => req.IsFinished);

                string res = "\n RequestAuthorization:";
                res += "\n finished: " + req.IsFinished;
                res += "\n granted :  " + req.Granted;
                res += "\n error:  " + req.Error;
                res += "\n deviceToken:  " + req.DeviceToken;
                Debug.Log(res);
            }
        }

        public override int SendNotification(Notification notification)
        {
            var notificationData = notification.NotificationData;
            iOSNotification iOSNotification = new iOSNotification()
            {
                Identifier = notificationData.id.ToString(),
                Title = notificationData.title,
                Body = notificationData.text,
                ThreadIdentifier = _channelID,

                Attachments = new List<iOSNotificationAttachment>(),

                Trigger = new iOSNotificationTimeIntervalTrigger()
                {
                    TimeInterval = new TimeSpan(DateTime.Now.AddSeconds(notification.GetDelayToShow()).Ticks),
                    Repeats = false,
                }
            };

            iOSNotificationCenter.ScheduleNotification(iOSNotification);
            return notificationData.id;
        }
    }
}
#endif