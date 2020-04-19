using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActivateable : MonoBehaviour, IActivateable
{
    // Private Variables
    public bool Activated { get; set; }

    public virtual void Activate()
    {
        Activated = true;
    }
    public virtual void Deactivate()
    {
        Activated = false;
    }
}
