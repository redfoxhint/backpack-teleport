using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWalkable
{
    void ToggleMovement(bool toggle);
    void SetWalkableVelocity(Vector3 velocity);
}
