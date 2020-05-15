using UnityEngine;
using System.Collections;

public class EntityPhysicsController : PhysicsCharacterController
{
    public void SetMoveDirection(Vector2 newDirection)
    {
        velocityVector = newDirection;
    }
}
