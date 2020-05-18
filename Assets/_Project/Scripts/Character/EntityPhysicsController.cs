using UnityEngine;
using System.Collections;
using PolyNav;

public class EntityPhysicsController : PhysicsCharacterController
{
    public void SetMoveDirection(Vector2 newDirection)
    {
        SetVelocity(newDirection);
    }

    protected override bool CanMove()
    {
        return DoMovement;
    }
}
