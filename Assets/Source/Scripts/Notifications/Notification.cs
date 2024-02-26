public abstract class Notification
{
    protected Notification(NotificationData notificationData)
    {
        NotificationData = notificationData;
    }

    public NotificationData NotificationData { get; private set; }

    public abstract int GetDelayToShow();

    public void SetID(int id)
    {
        NotificationData.id = id;
    }
}
