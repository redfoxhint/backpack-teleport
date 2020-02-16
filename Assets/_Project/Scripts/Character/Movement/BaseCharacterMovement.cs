using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseCharacterMovement : MonoBehaviour
{
    // Protected Variables
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

    protected virtual void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
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

        rBody.MovePosition(rBody.position + movement.normalized * distance);
    }

    protected virtual void CalculateMovement()
    {

    }
}
