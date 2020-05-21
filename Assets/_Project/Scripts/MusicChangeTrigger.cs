using BackpackTeleport.Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChangeTrigger : MonoBehaviour
{
    [SerializeField] private AudioFiles audioToFadeTo;
    [SerializeField] private float audioFadeTime = 5;

    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if(player != null)
        {
            if(!isActivated)
            {
                AudioManager.Instance.Crossfade(AudioType.MUSIC, audioToFadeTo, audioFadeTime);
                isActivated = true;
            }
            
        }
    }
}
