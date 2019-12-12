using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackTeleport.Character.PlayerCharacter;

public enum BackpackStates
{
	AIMING, INFLIGHT, IDLE, RETURNING, INHAND
}

[RequireComponent(typeof(BackpackAnimation), typeof(Rigidbody2D))]
public class Backpack : MonoBehaviour, IActivator
{
	// Public Variables
	[SerializeField] private float backpackMovementSpeed;
	[SerializeField] private AnimationCurve movementCurve;

    // Properties
    public bool CanBeAimed { get; set; }
    public bool ChainReady { get; set; }
    public Player Owner { get; private set; }

    // Private variables
    private Vector2 teleportDestination;

	// Components
	public BackpackStates currentState;
	private BackpackAnimation backpackAnimation;
	private BackpackChaining backpackChaining;
	private Rigidbody2D rBody;
	private Camera cam;

	// Tasks
	private Task launchTask;
	private Task returningTask;

	void Awake()
	{
		rBody = GetComponent<Rigidbody2D>();
		Owner = FindObjectOfType<Player>();
		backpackAnimation = GetComponent<BackpackAnimation>();
		backpackChaining = GetComponent<BackpackChaining>();
		cam = Camera.main;

		InitializeState(BackpackStates.INHAND);
	}

	private void Start()
	{
		CanBeAimed = true;
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
				if (Input.GetKeyDown(KeyCode.Mouse1))
				{
					if (launchTask != null)
						launchTask.Stop();

					Owner.Teleport(transform.position);
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
					if (!ChainReady)
					{
						backpackChaining.InitializeChain();
					}
					return;
				}

				if (Input.GetKeyDown(KeyCode.Mouse1))
				{
					Owner.Teleport(teleportDestination);
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
				rBody.position = Owner.RBody2D.position; // Performance check
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

		returningTask = new Task(ReturnToPlayerWithCurve(backpackMovementSpeed * 2, movementCurve));
		returningTask.Start();
	}

	public void Launch(Vector2 pos)
	{
		if (launchTask != null)
		{
			launchTask.Stop();
		}

		launchTask = new Task(MoveToPointWithCurve(backpackMovementSpeed, pos, movementCurve));
		launchTask.Start();

		teleportDestination = pos;
		rBody.position = Owner.RBody2D.position; // Set the position of the backpack to the players location so it looks like it spawns from the players center.
	}

	private IEnumerator MoveToPointWithCurve(float speed, Vector3 target, AnimationCurve animCurve)
	{
		Vector2 startPoint = rBody.position;
		float animationTimePosition = 0;
		target.z = 0;

		while (transform.position != target)
		{
			animationTimePosition += speed * Time.deltaTime;
			transform.position = Vector3.Lerp(startPoint, target, animCurve.Evaluate(animationTimePosition));
			transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
			yield return null;
		}

		InitializeState(BackpackStates.IDLE);

		yield break;
	}

	private IEnumerator ReturnToPlayerWithCurve(float speed, AnimationCurve animCurve)
	{
		Vector2 startPoint = rBody.position;
		float animationTimePosition = 0;

		while (Vector3.Distance(Owner.RBody2D.position, rBody.position) > 2f)
		{
			animationTimePosition += speed * Time.deltaTime;
			transform.position = Vector3.Lerp(startPoint, Owner.RBody2D.position, animCurve.Evaluate(animationTimePosition));
			transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
			yield return null;
		}

		InitializeState(BackpackStates.INHAND);

		yield break;
	}
}
