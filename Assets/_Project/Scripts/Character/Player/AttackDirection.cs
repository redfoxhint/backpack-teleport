using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDirection : MonoBehaviour
{
    public System.Action<GameObject> onColliderHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if(damageable != null)
        {
            onColliderHit.Invoke(other.gameObject);
        }
    }
}
