using UnityEngine;
using System.Collections;

public class EntityPhysicsController : PhysicsCharacterController
{
    protected override void Update()
    {
        //moveDirection = Vector2.zero;
        characterBase.SetAnimatorParameters(moveDirection);
    }

    public void SetMoveDirection(Vector2 newDirection)
    {
        moveDirection = newDirection;
    }
}
