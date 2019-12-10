using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Facing
{
	DOWN = 0,
	LEFT = 1,
	RIGHT = 2,
	UP = 3,
	UPRIGHT = 4,
	DOWNRIGHT = 5,
	UPLEFT = 6,
	DOWNLEFT = 7
}

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
		protected Facing currentFacing;
		protected Vector2 velocity;
		protected Vector2 lastVelocity;

		// Facing Direction Context
		[HideInInspector] public Vector2 down = new Vector2(0, -1);
		[HideInInspector] public Vector2 right = new Vector2(1, 0);
		[HideInInspector] public Vector2 left = new Vector2(-1, 0);
		[HideInInspector] public Vector2 upright = new Vector2(1f, 1f);
		[HideInInspector] public Vector2 up = new Vector2(0, 1);
		[HideInInspector] public Vector2 downright = new Vector2(1f, -1f);
		[HideInInspector] public Vector2 upleft = new Vector2(-1f, 1f);
		[HideInInspector] public Vector2 downleft = new Vector2(-1f, -1f);

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
			animator.SetFloat("Horizontal", Mathf.RoundToInt(velocity.x));
			animator.SetFloat("Vertical", Mathf.RoundToInt(velocity.y));
			animator.SetFloat("Speed", velocity.sqrMagnitude);
			animator.SetFloat("facingDirection", facingDirection);
		}

		public void HandleMovementDirection(Vector2 vel)
		{
			if (vel == Vector2.zero) return;

			vel = new Vector2(Mathf.RoundToInt(vel.x), Mathf.RoundToInt(vel.y));
			Debug.Log(vel);	

			if (vel == down) facingDirection = 0f;
			if (vel == right) facingDirection = 1f;
			if (vel == left) facingDirection = 2f;
			if (vel == upright) facingDirection = 3f;
			if (vel == up) facingDirection = 4f;
			if (vel == downright) facingDirection = 5f;
			if (vel == upleft) facingDirection = 6f;
			if (vel == downleft) facingDirection = 7f;

			lastVelocity = vel;
		}
	}
}
