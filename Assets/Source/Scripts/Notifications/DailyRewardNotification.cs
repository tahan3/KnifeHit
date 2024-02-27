public class DailyRewardNotification : Notification
{
    public DailyRewardNotification(NotificationData notificationData) : base(notificationData)
    {
    }

    public override int GetDelayToShow()
    {
        return 86400;
    }
}
