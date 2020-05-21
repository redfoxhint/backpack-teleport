using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActuationType { SINGLE, SEQUENCE }

public abstract class BaseActuator : MonoBehaviour
{
    [Header("Base Actuator Configuration")]
    [SerializeField] protected bool isDisabled;
    [SerializeField] protected bool stayActuated = false;
    [SerializeField] protected Sprite activatedSprite;
    [SerializeField] protected Sprite deactivatedSprite;
    [SerializeField] protected SpriteRenderer actuatorSpriteRenderer;

    // Private Variables
    public BaseActivateable ObjectToActivate { get; set; }
    public ActuationType ActuationType { get; set; }
    public bool IsActuated { get; set; }

    // Events
    public Action<BaseActuator> OnActivatedEvent;
    public Action<BaseActuator> OnDeactivatedEvent;

    public abstract void Activate();
    public abstract void Deactivate();
    public abstract void Reset();
}
