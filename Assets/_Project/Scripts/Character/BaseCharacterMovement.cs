using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 

    FACING DIRECTIONS:
    DOWN =      0
    UPRIGHT =   1
    DOWNRIGHT = 2
    LEFT =      3
    RIGHT =     4
    UPLEFT =    5
    DOWNLEFT =  6
    UP =        7

 */

namespace BackpackTeleport.Character
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Animator))]
	public class BaseCharacterMovement : MonoBehaviour
	{
		[Header("Movement Setup")]
		[SerializeField] protected float moveSpeed = 5f;

		// Private Variables
		protected float facingDirection;
		protected Vector2 velocity;
		protected Vector2 lastVelocity;

		// Facing Direction Context
		[HideInInspector] public Vector2 down = new Vector2(0, -1);
		[HideInInspector] public Vector2 upright = new Vector2(0, 1);
		[HideInInspector] public Vector2 downright = new Vector2(-1, -1);
		[HideInInspector] public Vector2 left = new Vector2(1, 0);
		[HideInInspector] public Vector2 right = new Vector2(-1, 0);
		[HideInInspector] public Vector2 upleft = new Vector2(1, 1);
		[HideInInspector] public Vector2 downleft = new Vector2(1, -1);
		[HideInInspector] Vector2 up = new Vector2(0, 1);

		// Properties
		public float FacingDirection { get => facingDirection; set => facingDirection = value; }
		public Vector2 Velocity { get => velocity; }
		public Vector2 LastVelocity { get => lastVelocity; }
		public Rigidbody2D RBody2D { get => rBody; }
		public float MoveSpeed { set => moveSpeed = value; }

		// Components
		protected Animator animator;
		protected Rigidbody2D rBody;

		public virtual void Awake()
		{
			if (rBody == null) rBody = GetComponent<Rigidbody2D>();
			if (animator == null) animator = GetComponent<Animator>();
		}

		public virtual void Update()
		{
			HandleMovementDirection(velocity);
			HandleMovementAnimation();
		}

		public virtual void FixedUpdate()
		{

		}

		public virtual void Move(Vector2 direction)
		{
			velocity = direction;
			rBody.velocity = velocity * moveSpeed;
		}

		public virtual void HandleMovementAnimation()
		{
			animator.SetFloat("Horizontal", velocity.x);
			animator.SetFloat("Vertical", velocity.y);
			animator.SetFloat("Speed", velocity.sqrMagnitude);
		}

		public void HandleMovementDirection(Vector2 vel)
		{
			if (vel == Vector2.zero) return;

			if (vel == new Vector2(0, -1)) facingDirection = 0f;
			if (vel == new Vector2(-1, 1)) facingDirection = 1f;
			if (vel == new Vector2(-1, -1)) facingDirection = 2f;
			if (vel == new Vector2(1, 0)) facingDirection = 3f;
			if (vel == new Vector2(-1, 0)) facingDirection = 4f;
			if (vel == Vector2.one) facingDirection = 5f;
			if (vel == new Vector2(1, -1)) facingDirection = 6f;
			if (vel == new Vector2(0, 1)) facingDirection = 7f;

			lastVelocity = vel;
		}
	}
}
