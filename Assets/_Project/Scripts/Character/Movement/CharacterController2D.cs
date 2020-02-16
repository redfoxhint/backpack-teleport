using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 

    FACING DIRECTIONS:
    DOWN =      0
    RIGHT =     1
    LEFT =      2
    UPRIGHT =   3
    UP =        4
    DOWNRIGHT = 5
    UPLEFT =    6
    DOWNLEFT =  7

 */

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : BaseCharacterMovement, IActivator
{
    [SerializeField] private float maxSpeed = 9;
    [SerializeField] private float acceleration = 75f;
    [SerializeField] private float deceleration = 70f;
    [SerializeField] private float facingDirection;

    private Animator animator;
    private Vector2 previousVelocity;

    public float FacingDirection { get => facingDirection; private set { } }

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void CalculateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(horizontalInput, verticalInput);
        input = Vector2.ClampMagnitude(input, 1); // Clamps the magnitude to 1 so directional movement speed is consistent in every direction.
        targetVelocity = input * maxSpeed;

        SetFacingDirection(targetVelocity.normalized);
        SetAnimatorParameters(horizontalInput, verticalInput, targetVelocity, facingDirection);
    }

    #region Animator Stuff

    // Facing Direction Context
    [HideInInspector] public Vector2 down = new Vector2(0, -1);
    [HideInInspector] public Vector2 right = new Vector2(1, 0);
    [HideInInspector] public Vector2 left = new Vector2(-1, 0);
    [HideInInspector] public Vector2 upright = new Vector2(1f, 1f);
    [HideInInspector] public Vector2 up = new Vector2(0, 1);
    [HideInInspector] public Vector2 downright = new Vector2(1f, -1f);
    [HideInInspector] public Vector2 upleft = new Vector2(-1f, 1f);
    [HideInInspector] public Vector2 downleft = new Vector2(-1f, -1f);

    public void SetFacingDirection(Vector2 moveDir)
    {
        if (moveDir == Vector2.zero) return;

        moveDir = new Vector2(Mathf.RoundToInt(moveDir.x), Mathf.RoundToInt(moveDir.y));

        if (moveDir == down) SetFacingDirection(0f);
        if (moveDir == right) SetFacingDirection(1f);
        if (moveDir == left) SetFacingDirection(2f);
        if (moveDir == upright) SetFacingDirection(3f);
        if (moveDir == up) SetFacingDirection(4f);
        if (moveDir == downright) SetFacingDirection(5f);
        if (moveDir == upleft) SetFacingDirection(6f);
        if (moveDir == downleft) SetFacingDirection(7f);

        previousVelocity = moveDir;
    }

    public void SetFacingDirection(float newDirection)
    {
        if (newDirection < 0 || newDirection > 7) return; // In case a number which is not a direction is passed in.
        facingDirection = newDirection;
    }

    private void SetAnimatorParameters(float horizontal, float vertical, Vector2 movement, float facingDir)
    {
        if (animator != null)
        {
            animator.SetFloat("Horizontal", Mathf.RoundToInt(horizontal));
            animator.SetFloat("Vertical", Mathf.RoundToInt(vertical));
            animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetFloat("facingDirection", facingDir);
        }
    }
    #endregion
}
