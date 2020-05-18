using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private CircleCollider2D circleCol;
    [SerializeField] private LayerMask damageFilter;
    public void RangedAttack(Vector2 direction, float damage)
    {
        GameObject projectileObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        EnemyProjectile projectile = projectileObj.GetComponent<EnemyProjectile>();
        projectile.InitProjectile(direction, damage);
        Debug.Log(direction);
    }
    public void MeleeAttack(float damage)
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, circleCol.radius * 2f, damageFilter);

        if (hit)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            damageable.TakeDamage(transform, damage);
        }
    }
}
