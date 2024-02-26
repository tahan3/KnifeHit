public class ScheduledNotification : Notification
{
    public ScheduledNotification(NotificationData notificationData) : base(notificationData)
    {

    }

    public override int GetDelayToShow()
    {
        return NotificationData.delayToShow;
    }
}
