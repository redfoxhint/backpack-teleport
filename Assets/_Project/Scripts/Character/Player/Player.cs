using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AimingAnimation))]
public class Player : MonoBehaviour
{
	// Public Variables
	[SerializeField] private Transform playerCenter;
	[SerializeField] private GameObject teleportEffect;
	[SerializeField] private VoidEvent onTeleportEvent;
	[SerializeField] private VoidEvent onThrowEvent;

	// Private Variables
	private Vector2 pointA;
	private Vector2 pointB;
	private bool isAiming;
	private float backpackTravelDistance;

	// Components
	[HideInInspector] public Rigidbody2D rBody;
	[SerializeField] private Backpack backpack;
	private PlayerMovement playerMovement;
	private PlayerAnimations playerAnimations;
	private PlayerStats playerStats;
	private ForceManager statManager;
	private AimingAnimation aimingAnimation;
	private DottedLine dottedLine;
	private TrailRenderer trailRenderer;
	private Camera cam;

	void Awake()
	{
		rBody = GetComponent<Rigidbody2D>();
		aimingAnimation = GetComponent<AimingAnimation>();
		playerAnimations = GetComponent<PlayerAnimations>();
		playerMovement = GetComponent<PlayerMovement>();
		playerStats = GetComponent<PlayerStats>();
		trailRenderer = GetComponent<TrailRenderer>();
		statManager = ForceManager.Instance;
		dottedLine = DottedLine.Instance;
		cam = Camera.main;
	}

	void Start()
	{
		isAiming = false;
		trailRenderer.enabled = false;
	}

	private void Update()
	{
		if (ThrowBackPack() && backpack.CanBeAimed)
		{
			backpack.InitializeState(BackpackStates.AIMING);
			isAiming = true;
			StartCoroutine(AimBackpack());
		}

		if (isAiming)
		{
			HandleBackpackAiming();
			aimingAnimation.DrawGraphics(pointA, pointB);
			aimingAnimation.IsAiming = true;
		}
		else
		{
			aimingAnimation.IsAiming = false;
		}
	}

	private void HandleBackpackAiming()
	{
		if (statManager.HasForceRequired(backpackTravelDistance))
		{
			aimingAnimation.UpdateUI(backpackTravelDistance, Color.white);
		}
		else
		{
			aimingAnimation.UpdateUI(backpackTravelDistance, Color.red);
		}

		if (aimingAnimation.AimAngle > 90f || aimingAnimation.AimAngle < -90f)
		{
			aimingAnimation.RotateText(-180f);
		}
		else
		{
			aimingAnimation.RotateText(0f);
		}
	}

	private IEnumerator AimBackpack()
	{
		while (Input.GetKey(KeyCode.Space))
		{
			pointA = playerCenter.transform.position;
			pointB = cam.ScreenToWorldPoint(Input.mousePosition);

			if (Input.GetKeyDown(KeyCode.R))
			{
				backpack.InitializeState(BackpackStates.INHAND);
				isAiming = false;
				yield break;
			}

			backpackTravelDistance = (pointB - pointA).sqrMagnitude;

			yield return new WaitForSeconds(Time.deltaTime);
		}

		// Throw the bag only if we have enough force points and strength.

		if (statManager.HasForceRequired(backpackTravelDistance))
		{
			isAiming = false;
			onThrowEvent.Raise();
			backpack.Launch(pointB);
			backpack.InitializeState(BackpackStates.INFLIGHT);
			playerAnimations.TriggerThrowing(playerMovement.FaceDirection);
			yield break;
		}
		else
		{
			isAiming = false;
			backpack.InitializeState(BackpackStates.INHAND);
			yield break;
		}
	}

	private bool ThrowBackPack()
	{
		return Input.GetKeyDown(KeyCode.Space) && backpack.currentState == BackpackStates.INHAND;
	}

	public void Teleport(Vector2 pos)
	{
		rBody.position = pos;
		playerStats.UseTeleport();
		onTeleportEvent.Raise();
		trailRenderer.enabled = true;
		GameObject newTeleportEffect = Instantiate(teleportEffect, pos, teleportEffect.transform.rotation);
		Destroy(newTeleportEffect, 2f);
		Invoke("TurnTrailRendererOff", 0.5f);
	}

	private void TurnTrailRendererOff()
	{
		trailRenderer.enabled = false;
	}
}
