using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 5f;
	public float FaceDirection { get { return faceDirection; } }
	public Vector2 Movement { get => movement; set => movement = value;}

	private float faceDirection;

	private Animator animator;
	private PlayerAnimations playerAnimation;
	private Rigidbody2D rBody;

	Vector2 movement;

	void Awake()
	{
		rBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		playerAnimation = GetComponent<PlayerAnimations>();
	}

	void Update()
	{
		// Input
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		movement = new Vector2(horizontal, vertical);
		movement = movement.normalized;

		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetFloat("Speed", movement.sqrMagnitude);

		HandleMovementDirection(movement.normalized);

	}

	private void FixedUpdate()
	{
		rBody.MovePosition(rBody.position + movement * moveSpeed * Time.fixedDeltaTime);
	}

	private void HandleMovementDirection(Vector2 movementDirection)
	{
		if (movementDirection == Vector2.zero) return;

		if (movementDirection == Vector2.one) // Up Left
		{
			faceDirection = 5f;
		}

		if (movementDirection == new Vector2(-1, 1)) // Up Right
		{
			faceDirection = 1f;
		}

		if (movementDirection == new Vector2(1, -1)) // Down Left
		{
			faceDirection = 6f;
		}

		if (movementDirection == new Vector2(-1, -1)) // Down Right
		{
			faceDirection = 2f;
		}

		if (movementDirection == new Vector2(0, 1)) // Up
		{
			faceDirection = 7f;
		}

		if (movementDirection == new Vector2(0, -1)) // Down
		{
			faceDirection = 0f;
		}

		if (movementDirection == new Vector2(1, 0)) // Left
		{
			faceDirection = 3f;
		}

		if (movementDirection == new Vector2(-1, 0)) // Right
		{
			faceDirection = 4f;
		}

		playerAnimation.SetIdleSprite(faceDirection);
	}

}
