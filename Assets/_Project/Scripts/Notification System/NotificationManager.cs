using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NotificationManager : Singleton<NotificationManager>
{
    [SerializeField] private NotificationUI notificationUI;
    private Notification notification;

    public Queue<Notification> NotificationBuffer { get; private set; }

    private void Awake()
    {
        NotificationBuffer = new Queue<Notification>();
    }

    private void OnEnable()
    {
        notificationUI.OnAnimationFinished += OnNotificationDie;
        GameEvents.onNotificationCreated.AddListener(CreateNotification);
    }

    private void OnDisable()
    {
        notificationUI.OnAnimationFinished -= OnNotificationDie;
    }

    public void CreateNotification(Notification notification)
    {
        NotificationBuffer.Enqueue(notification);

        if(NotificationBuffer.Count == 1)
        {
            ShowNextNotification();
        }
    }

    private void Update()
    {
        if(Keyboard.current.kKey.wasPressedThisFrame)
        {
            Notification testNotif = new Notification(Random.Range(0, 1500f).ToString(), NotificationType.TEXT);
            CreateNotification(testNotif);
        }
    }

    private void OnNotificationDie()
    {
        NotificationBuffer.Dequeue();
        ShowNextNotification();
    }

    private void ShowNextNotification()
    {
        if(NotificationBuffer.Count > 0)
        {
            notification = NotificationBuffer.Peek();
            notificationUI.ShowNotification(notification);
        }
    }
}
