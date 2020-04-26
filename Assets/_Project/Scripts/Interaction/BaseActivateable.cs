using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivateableMode { SINGLE, SEQUENCE }

public abstract class BaseActivateable : MonoBehaviour, IActivateable
{
    [SerializeField] protected ActivateableMode activationType;
    public Action<BaseActivateable> OnActuatedEvent;

    // Private Variables
    public bool Actuated { get; set; }

    public virtual void Actuate()
    {
        Actuated = true;
    }

    public virtual void Deactivate()
    {
        Actuated = false;
    }
}
