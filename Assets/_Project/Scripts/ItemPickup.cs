using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.berryAmount += 1;
            AudioManager.Instance.PlaySoundEffect(AudioFiles.SFX_GoldPickup);
            GameEvents.onBerryPickedUp?.Invoke();
            Destroy(gameObject);
        }
    }
}
