using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActivateable : MonoBehaviour, IActivateable
{
    // Private Variables
    protected bool activated;

    public virtual void Activate()
    {
        activated = true;
    }
    public virtual void Deactivate()
    {
        activated = false;
    }
}
