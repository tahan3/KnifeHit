using Notifications;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using Zenject;

public class NotificationInstaller : MonoInstaller
{
    [SerializeField] private string _pathToIcon;
    [SerializeField] private string _channelID;
    [SerializeField] private List<NotificationData> _notificationDatas;

    private NotificationController _notificationsController;

#if UNITY_ANDROID || UNITY_IOS
    public async override void InstallBindings()
    {
#if UNITY_ANDROID
        Debug.Log("android");
        _notificationsController = new AndroidNotificationsController(_pathToIcon, _channelID);
#elif UNITY_IOS
        _notificationsController = new IOSNotificationsController(_pathToIcon, _channelID);
#endif
        for (int i = 0; i < _notificationDatas.Count; i++)
        {
            var scheduleNotification = new ScheduledNotification(_notificationDatas[i]);

            _notificationsController.AddNotification(scheduleNotification);
        }

        await _notificationsController.RequestPermission();
        _notificationsController.SendNotifications();
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
