using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum ActivateableMode { SINGLE, SEQUENCE }

public abstract class BaseActivateable : MonoBehaviour, IActivateable
{
    [Header("Base Activateable Configuration")]
    [SerializeField] protected ActivateableMode activationMode;
    [SerializeField] protected bool isGated;
    [SerializeField] protected List<BaseActivateable> gates = new List<BaseActivateable>();
    [SerializeField] protected BoxCollider2D colliderToDisable;
    [SerializeField] protected Animator anim;

    // Events
    public Action<BaseActivateable> OnActuatedEvent;

    // Private Variables
    public bool IsUnlocked { get; set; }

    public bool IsPartOfSequence
    {
        set
        {
            activationMode = ActivateableMode.SEQUENCE;
        }
    }

    public virtual void Activate()
    {
        IsUnlocked = true;
    }

    public virtual void Deactivate()
    {
        IsUnlocked = false;
    }

    public bool CheckGatesOpen()
    {
        if (!isGated) return true;

        if(gates != null && gates.Count > 0)
        {
            bool allOpen = gates.Any() && gates.All(item => item.IsUnlocked);
            if (allOpen) return true;
        }

        return false;
    }

    public abstract void OnActuatorActuated(BaseActuator actuator);
    public abstract void OnResetActivatorActuated(BaseActuator actuator);
    public abstract void Reset();
}
