using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationType
{
    NORMAL,
    FULLSCREEN
}

[System.Serializable]
public class Notification 
{
    [TextArea]public string NotificationText;
    public NotificationType NotificationType;
    public Sprite NotificationSprite;

    public Notification(string notificationText, NotificationType notificationType, Sprite sprite)
    {
        NotificationText = notificationText;
        NotificationType = notificationType;
        NotificationSprite = sprite;
    }

    public Notification(string notificationText, NotificationType notificationType)
    {
        NotificationText = notificationText;
        NotificationType = notificationType;
    }
}
