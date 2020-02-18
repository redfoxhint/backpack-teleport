using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseObjectMovement : MonoBehaviour
{
    // Protected Variables
    [SerializeField] protected LayerMask collisionFilter;
    protected bool careAboutAnimator;
    protected Vector2 targetVelocity;
    protected Vector2 velocity;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    // Private Variables
    private const float minMoveDistance = 0.001f;
    private const float skinWidth = 0.01f;

    // Components
    protected Rigidbody2D rBody;
    protected ContactFilter2D contactFilter;
    protected Collider2D col2D;

    protected virtual void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        contactFilter.SetLayerMask(collisionFilter);
    }

    protected virtual void Update()
    {
        targetVelocity = Vector2.zero;
        CalculateMovement();
    }

    private void FixedUpdate()
    {
        velocity = targetVelocity;
        Movement(velocity * Time.fixedDeltaTime);
    }

    private void Movement(Vector2 movement)
    {
        if (movement == Vector2.zero) return;

        float distance = movement.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rBody.Cast(movement, contactFilter, hitBuffer, distance + skinWidth);
            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                float modifiedDistance = hitBufferList[i].distance - skinWidth;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        Vector2 targetPosition = rBody.position + movement.normalized * distance;

        rBody.MovePosition(targetPosition);
    }

    protected Vector2 FixPosition()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, col2D.bounds.size, 0, collisionFilter);

        foreach (Collider2D hit in hits)
        {
            if (hit == col2D)
            {
                continue;
            }

            ColliderDistance2D colliderDistance = hit.Distance(col2D);
            if (colliderDistance.isOverlapped)
            {
                Vector2 fixedPoint = colliderDistance.pointA - colliderDistance.pointB;
                transform.Translate(fixedPoint);
                return fixedPoint;
            }
        }

        return transform.position;
    }

    protected virtual void CalculateMovement() {  }
}
