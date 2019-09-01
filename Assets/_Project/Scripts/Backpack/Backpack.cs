using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackTeleport.Character.PlayerCharacter;

public enum BackpackStates
{
	AIMING, INFLIGHT, IDLE, RETURNING, INHAND
}

[RequireComponent(typeof(BackpackAnimation))]
public class Backpack : MonoBehaviour
{
	// Public Variables
	[SerializeField] private float backpackMovementSpeed;
	public bool CanBeAimed { get { return canBeAimed; } set { canBeAimed = value; } }
	public bool ChainReady { get { return chainReady; } set { chainReady = value; } }
	public Player Owner { get { return owner; } }

	// Private variables
	private Vector2 teleportDestination;
	private bool chainReady;
	private bool canBeAimed;

	// Components
	public BackpackStates currentState;
	private BackpackAnimation backpackAnimation;
	private BackpackChaining backpackChaining;
	private Rigidbody2D rBody;
	private Player owner;
	private Camera cam;

	// Tasks
	private Task launchTask;
	private Task returningTask;

	void Awake()
	{
		rBody = GetComponent<Rigidbody2D>();
		owner = FindObjectOfType<Player>();
		backpackAnimation = GetComponent<BackpackAnimation>();
		backpackChaining = GetComponent<BackpackChaining>();
		cam = Camera.main;

		InitializeState(BackpackStates.INHAND);
	}

	private void Start()
	{
		canBeAimed = true;
	}

	void Update()
	{
		UpdateStates();
	}

	private void UpdateStates()
	{
		switch (currentState)
		{
			case BackpackStates.AIMING:
				break;

			case BackpackStates.INFLIGHT:
				if (Input.GetKeyDown(KeyCode.Space))
				{
					if (launchTask != null)
						launchTask.Stop();

					owner.Teleport(transform.position);
					InitializeState(BackpackStates.INHAND);
					return;
				}
				break;

			case BackpackStates.IDLE:

				/* -If we have a chain placed, then run the chain code and return out of the main states main update method. 
				   - We also use this to prevent from throwing bag while its part of a chain.
				*/

				if (backpackChaining.ChainComplete)
				{
					if (!chainReady)
					{
						backpackChaining.InitializeChain();
					}
					return;
				}

				if (Input.GetKeyDown(KeyCode.Space))
				{
					owner.Teleport(teleportDestination);
					InitializeState(BackpackStates.INHAND);
				}

				else if (Input.GetKey(KeyCode.LeftShift))
				{
					backpackChaining.PlaceMarkerAtPosition(cam.ScreenToWorldPoint(Input.mousePosition));
				}

				else if (Input.GetKeyDown(KeyCode.R))
				{
					InitializeState(BackpackStates.RETURNING);
					return;
				}
				break;

			case BackpackStates.INHAND:
				rBody.position = owner.RBody2D.position; // Performance check
				break;

			case BackpackStates.RETURNING:
				// TODO: Returning state
				break;
		}
	}

	public void InitializeState(BackpackStates newState)
	{
		switch (newState)
		{
			case BackpackStates.AIMING:
				Init_AimingState();
				break;

			case BackpackStates.INFLIGHT:
				Init_InFlightState();
				break;

			case BackpackStates.IDLE:
				Init_IdleState();
				break;

			case BackpackStates.INHAND:
				Init_InHandState();
				break;

			case BackpackStates.RETURNING:
				Init_ReturningState();
				break;
		}
	}

	private void Init_AimingState()
	{
		currentState = BackpackStates.AIMING;
	}

	private void Init_InFlightState()
	{
		currentState = BackpackStates.INFLIGHT;
		backpackAnimation.ToggleTrails(true);
		backpackAnimation.SwitchHasBackback(false);
		backpackAnimation.ShowBackpack();
	}

	private void Init_IdleState()
	{
		currentState = BackpackStates.IDLE;
		backpackAnimation.ToggleTrails(false);
	}

	private void Init_InHandState()
	{
		currentState = BackpackStates.INHAND;
		backpackAnimation.ToggleTrails(false);
		backpackAnimation.SwitchHasBackback(true);
		backpackAnimation.RippleEffect(transform.position);
		backpackAnimation.HideBackpack();
	}

	private void Init_ReturningState()
	{
		currentState = BackpackStates.RETURNING;

		returningTask = new Task(ReturnToPlayerOverSpeed());
		returningTask.Start();
	}

	public void Launch(Vector2 pos)
	{
		if (launchTask != null)
		{
			launchTask.Stop();
		}

		launchTask = new Task(MoveToPointOverSpeed(pos, backpackMovementSpeed));
		launchTask.Start();

		teleportDestination = pos;
		rBody.position = owner.RBody2D.position; // Set the position of the backpack to the players location so it looks like it spawns from the players center.
	}

	IEnumerator MoveToPointOverSpeed(Vector3 endPoint, float speed)
	{
		Vector2 dir = (transform.position - endPoint).normalized;

		while (rBody.position != (Vector2)endPoint)
		{
			rBody.position = Vector2.MoveTowards(rBody.position, endPoint, backpackMovementSpeed * Time.fixedDeltaTime);
			yield return new WaitForFixedUpdate();
		}

		InitializeState(BackpackStates.IDLE);
		rBody.AddForce(-dir * 2000f);
		yield break;
	}

	IEnumerator ReturnToPlayerOverSpeed()
	{
		while (Vector3.Distance(owner.RBody2D.position, rBody.position) > 2f)
		{
			rBody.position = Vector2.MoveTowards(rBody.position, owner.RBody2D.position, backpackMovementSpeed * Time.fixedDeltaTime);
			yield return new WaitForFixedUpdate();
		}

		InitializeState(BackpackStates.INHAND);
		yield break;
	}
}
