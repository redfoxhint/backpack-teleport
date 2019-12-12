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

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    // Inspector Fields
    [Header("Character Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float movementDampingAmount = 15f;

    // Private Variables
    private float currentSpeed;
    private float facingDirection;
    private Vector2 smoothedVelocity; // Smoothed movement direction

    // Properties
    public Vector2 LastVelocity { get; private set; }
    public Vector2 MovementVector { get; private set; } // Normalized movement direction

    // Facing Direction Context
    [HideInInspector] public Vector2 down = new Vector2(0, -1);
    [HideInInspector] public Vector2 right = new Vector2(1, 0);
    [HideInInspector] public Vector2 left = new Vector2(-1, 0);
    [HideInInspector] public Vector2 upright = new Vector2(1f, 1f);
    [HideInInspector] public Vector2 up = new Vector2(0, 1);
    [HideInInspector] public Vector2 downright = new Vector2(1f, -1f);
    [HideInInspector] public Vector2 upleft = new Vector2(-1f, 1f);
    [HideInInspector] public Vector2 downleft = new Vector2(-1f, -1f);

    // Components
    protected Animator animator;
    protected Rigidbody2D rBody;
    private Camera cam;

    protected virtual void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cam = Camera.main;
    }

    protected virtual void Update()
    {
        
    }

    public void Move(Vector2 moveDirection)
    {
        MovementVector = moveDirection.normalized;
        SetFacingDirection(moveDirection.normalized);

        smoothedVelocity = Vector2.Lerp(smoothedVelocity, moveDirection.normalized * moveSpeed, Time.deltaTime * movementDampingAmount);
        rBody.velocity = smoothedVelocity;

        SetAnimatorParameters();
    }

    private void SetAnimatorParameters()
    {
        if (animator != null)
        {
            animator.SetFloat("Horizontal", Mathf.RoundToInt(MovementVector.x));
            animator.SetFloat("Vertical", Mathf.RoundToInt(MovementVector.y));
            animator.SetFloat("Speed", MovementVector.sqrMagnitude);
            animator.SetFloat("facingDirection", facingDirection);
        }
    }

    private void SetFacingDirection(Vector2 moveDir)
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

        LastVelocity = moveDir;
    }

    public void SetFacingDirection(float newDirection)
    {
        if (newDirection < 0 || newDirection > 7) return; // In case a number which is a direction is passed in.
        facingDirection = newDirection;
    }

    public void ApplyKnockback(Transform other, float force)
    {
        Vector2 direction = other.position - transform.position;
        rBody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }
}
