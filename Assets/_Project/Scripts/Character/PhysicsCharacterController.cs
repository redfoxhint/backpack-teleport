using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class PhysicsCharacterController : MonoBehaviour, IWalkable
{
    [Header("Character Controller Configuration")]
    [SerializeField] protected float moveSpeed = 60f;

    // Properties
    public bool DoMovement { get; set; }

    protected Vector2 velocityVector;
    protected Vector2 previousDirection;

    protected Rigidbody2D rBody;
    protected InputManager input;
    protected CharacterAnimator characterBase;

    protected virtual void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        characterBase = GetComponent<CharacterAnimator>();
        input = InputManager.Instance;
        ToggleMovement(true);
    }

    protected virtual void FixedUpdate()
    {
        if(DoMovement)
        {
            rBody.velocity = velocityVector * moveSpeed;
            characterBase.SetAnimatorParameters(velocityVector);
        }
    }
    
    protected virtual bool CanMove()
    {
        return DoMovement && GameManager.Instance.PlayerControl;
    }

    public void SetVelocity(Vector3 _velocityVector)
    {
        velocityVector = _velocityVector;
    }

    public void Disable()
    {
        this.enabled = false;
        rBody.velocity = Vector3.zero;
    }

    public void Enable()
    {
        this.enabled = true;
    }

    public void ToggleMovement(bool toggle)
    {
        DoMovement = toggle;

        if(!toggle)
            SetWalkableVelocity(Vector3.zero);

    }

    public void SetWalkableVelocity(Vector3 velocity)
    {
        rBody.velocity = velocity;
    }
}
