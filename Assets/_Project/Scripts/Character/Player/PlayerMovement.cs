using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimations))]
public class PlayerMovement : BaseCharacterMovement
{
	private PlayerAnimations playerAnimations;

	public override void Awake()
	{
		base.Awake();
		playerAnimations = GetComponent<PlayerAnimations>();
	}

	public override void Update()
	{
		// Input
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector2 vel = new Vector2(horizontal, vertical);
		vel = Vector2.ClampMagnitude(vel, 1);
		base.Update();
		playerAnimations.SetIdleSprite(facingDirection);
		Move(vel);
	}
}
