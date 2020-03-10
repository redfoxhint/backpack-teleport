using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Knockback : MonoBehaviour
{
	[Header("Knocback Setup")]
	[Tooltip("The amount of force used to knockback.")]
	[SerializeField] private float knockbackAmount;
	[Tooltip("How long the character will be knockbacked for.")]
	[SerializeField] private float knockbackTime;

	// Private Variables
	private Vector2 knockbackDirection;
	private float knockbackCounter;
	private bool knockbackTimerStart = false;
	private bool doKnockback;

	// Componenents
	private Rigidbody2D rBody;

	// Events
	public Action OnKnockbackStarted;
	public Action OnKnockbackFinished;

	public float KnockbackCounter => knockbackCounter;
	public float KnockbackAmount { get { return knockbackAmount; } set { knockbackAmount = value; } }
	public float KnockbackTime { get { return knockbackTime; } set { knockbackTime = value; } }

	private void Awake()
	{
		rBody = GetComponent<Rigidbody2D>();
	}


	private void Update()
	{
		if (knockbackCounter > 0 && knockbackTimerStart)
		{
			knockbackCounter -= Time.deltaTime;
		}
		else if (knockbackCounter <= 0 && knockbackTimerStart)
		{
			knockbackTimerStart = false;
			OnKnockbackFinished?.Invoke();
		}
	}

	private void FixedUpdate()
	{
		if (doKnockback)
		{
			rBody.AddForce(knockbackDirection * knockbackAmount, ForceMode2D.Impulse);
			doKnockback = false;
		}
	}

	public void ApplyKnockback(Transform from)
	{
		rBody.velocity = Vector2.zero;
		doKnockback = true;
		knockbackCounter = knockbackTime;
		knockbackTimerStart = true;
		knockbackDirection = CalculateKnockbackDirection(from);
		OnKnockbackStarted?.Invoke();
	}

	private Vector2 CalculateKnockbackDirection(Transform origin)
	{
		Vector3 direction = origin.position.DirectionTo(transform.position);
		return direction;
	}
}
