using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Notification firstBerryPickupNotification;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(GameManager.Instance.FirstPickup)
        {
            GameEvents.onNotificationCreated.Invoke(firstBerryPickupNotification);
            GameManager.Instance.FirstPickup = false;
        }

        if(other.CompareTag("Player"))
        {
            GameManager.Instance.berryAmount += 1;
            AudioManager.Instance.PlaySoundEffect(AudioFiles.SFX_GoldPickup);
            GameEvents.onBerryPickedUp?.Invoke();
            Destroy(gameObject);
        }
    }
}
