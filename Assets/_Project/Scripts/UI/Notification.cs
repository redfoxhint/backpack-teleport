using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationType
{
    TEXT,
    BUTTON
}

public class Notification
{
    public string NotificationText { get; private set; }
    public NotificationType NotificationType { get; private set; }

    public Notification(string notificationText, NotificationType notificationType)
    {
        NotificationText = notificationText;
        NotificationType = notificationType;
    }
}
