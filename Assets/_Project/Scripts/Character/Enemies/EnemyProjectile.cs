using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private LayerMask filter;

    private Rigidbody2D rBody;
    private CircleCollider2D circleCollider;
    private float nextDamage;

    private Vector2 velocity;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void InitProjectile(Vector2 _velocity, float damage)
    {
        velocity = _velocity;
        nextDamage = damage;
    }

    private void Update()
    {
        transform.Translate(velocity * projectileSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, circleCollider.radius, filter);

        if (hit)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            damageable.TakeDamage(transform, nextDamage);
            Destroy(gameObject);
        }
    }

}
