using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterBase))]
public class PhysicsCharacterController : MonoBehaviour
{
    [Header("Character Controller Configuration")]
    [SerializeField] private LayerMask dashFilter;
    [SerializeField] private float moveSpeed = 60f;

    [Header("Dash Configuration")]
    [SerializeField] private float dashAmount;

    // Properties
    public bool DoMovement { get; set; }

    private bool isDashButtonDown = false;
    private Vector2 moveDirection;
    private Vector2 previousDirection;

    private Rigidbody2D rBody;
    private InputManager input;
    private CharacterBase characterBase;
    private DashAbility dashAbility;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        characterBase = GetComponent<CharacterBase>();
        dashAbility = GetComponent<DashAbility>();
        input = InputManager.Instance;
    }

    private void Update()
    {
        moveDirection = Vector2.zero;
        GetPlayerInput();

        characterBase.SetAnimatorParameters(moveDirection);

        bool dashButton = input.InputActions.Player.DashAttack.triggered;
        if (dashButton)
        {
            isDashButtonDown = true;
        }
    }

    private void FixedUpdate()
    {
        rBody.velocity = moveDirection * moveSpeed;

        if(isDashButtonDown)
        {
            Dash();
            isDashButtonDown = false;
        }
    }

    private void GetPlayerInput()
    {
        if(CanMove())
        {
            float moveX = input.MovementInput.x;
            float moveY = input.MovementInput.y;

            moveDirection = new Vector2(moveX, moveY).normalized;
            previousDirection = moveDirection;
        }
    }
    
    private bool CanMove()
    {
        return DoMovement;
    }

    public void Dash()
    {
        dashAbility.Dash(rBody, moveDirection, dashAmount, dashFilter);
    }
}
