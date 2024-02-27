using Notifications;
using System.Collections;
using System.Collections.Generic;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

using UnityEngine;
using Zenject;

public class NotificationInstaller : MonoInstaller
{
    [SerializeField] private string _pathToIcon;
    [SerializeField] private string _channelID;
    [SerializeField] private List<NotificationData> _notificationDatas;
    [SerializeField] private NotificationData _dailyRewardNotificationData;

    private NotificationController _notificationsController;

#if UNITY_ANDROID || UNITY_IOS
    public async override void InstallBindings()
    {
#if UNITY_ANDROID
        _notificationsController = new AndroidNotificationsController(_pathToIcon, _channelID);
#elif UNITY_IOS
        _notificationsController = new IOSNotificationsController(_pathToIcon, _channelID);
#endif
        for (int i = 0; i < _notificationDatas.Count; i++)
        {
            var scheduleNotification = new ScheduledNotification(_notificationDatas[i]);

            _notificationsController.AddNotification(scheduleNotification);
        }

        var dailyRewardNotification = new DailyRewardNotification(_dailyRewardNotificationData);
        _notificationsController.AddNotification(dailyRewardNotification);


        await _notificationsController.RequestPermission();
        //_notificationsController.SendNotifications();
    }

    private void OnApplicationPause(bool isPause)
    {
        if (isPause)
        {
            _notificationsController.SendNotifications();
        }
        else
        {
            _notificationsController.CancelNotifications();
        }
    }
#endif
    }
