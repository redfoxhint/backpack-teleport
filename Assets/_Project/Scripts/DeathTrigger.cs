using BackpackTeleport.Character;
using BackpackTeleport.Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if(player)
        {
            player.GetComponent<BaseDamageable>().Kill(false, true);
        }

        BaseEnemy baseEnemy = other.GetComponent<BaseEnemy>();

        if(baseEnemy)
        {
            baseEnemy.GetComponent<BaseDamageable>().Kill(true, true);
        }
    }
}
