using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivatedNotification : MonoBehaviour
{
    [SerializeField] private Notification notification;
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (activated) return;

            activated = true;
            GameEvents.onNotificationCreated?.Invoke(notification);
        }
    }
}
