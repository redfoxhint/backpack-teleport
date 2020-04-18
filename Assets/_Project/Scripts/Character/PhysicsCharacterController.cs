using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterBase))]
public class PhysicsCharacterController : MonoBehaviour
{
    [Header("Character Controller Configuration")]
    [SerializeField] protected float moveSpeed = 60f;

    // Properties
    public bool DoMovement { get; set; }


    protected Vector2 moveDirection;
    protected Vector2 previousDirection;

    protected Rigidbody2D rBody;
    protected InputManager input;
    protected CharacterBase characterBase;

    protected virtual void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        characterBase = GetComponent<CharacterBase>();
        input = InputManager.Instance;
        DoMovement = true;
    }

    protected virtual void Update()
    {
        moveDirection = Vector2.zero;
        characterBase.SetAnimatorParameters(moveDirection);
    }

    protected virtual void FixedUpdate()
    {
        rBody.velocity = moveDirection * moveSpeed;
    }
    
    protected virtual bool CanMove()
    {
        return DoMovement && GameManager.Instance.PlayerControl;
    }

    
}
