using System.Collections;
using UnityEngine;
using BackpackTeleport.Character.Enemy;

namespace BackpackTeleport.Character.PlayerCharacter
{
	[RequireComponent(typeof(AimingAnimation))]
	public class Player : BaseCharacter
	{
		// Inspector Fields
		[Header("Backpack Settings")]
		[Space]
		[SerializeField] private float maxThrowDistance = 300f;
		[SerializeField] private KeyCode throwKey = KeyCode.LeftControl;

		[Header("Area of Effect Damage Settings")]
		[Space]
		[SerializeField] private float areaDamageAmount = 3f;
		[SerializeField] private float areaDamageRadius = 4f;

		[Header("Events")]
		[Space]
		[SerializeField] private VoidEvent onTeleportEvent;
		[SerializeField] private VoidEvent onThrowEvent;
		[SerializeField] private StatEvent onTakeDamage;
		[SerializeField] private StatEvent onTeleportStatEvent;

		[Header("Other")]
		[Space]
		[SerializeField] private Transform playerCenter;
		[SerializeField] private GameObject teleportEffect;
		[SerializeField] private Stat healthStat;
		[SerializeField] private Stat energyStat;

		// Public Variables


		// Private Variables
		private Vector2 pointA;
		private Vector2 pointB;

		private bool isAimingBackpack;

		private float calculatedBackpackTravelDistance;

		// Components
		private Backpack backpack;
		private PlayerAnimations playerAnimations;
		private AimingAnimation aimingAnimation;
		private DottedLine dottedLine;
		private TrailRenderer trailRenderer;
		private Camera cam;

		public override void Awake()
		{
			base.Awake();

			backpack = FindObjectOfType<Backpack>();
			aimingAnimation = GetComponent<AimingAnimation>();
			playerAnimations = GetComponent<PlayerAnimations>();
			trailRenderer = GetComponent<TrailRenderer>();
			dottedLine = DottedLine.Instance;
			cam = Camera.main;
		}

		public override void Start()
		{
			base.Start();

			isAimingBackpack = false;
			trailRenderer.enabled = false;
		}

		public override void Update()
		{
			HandleMovement();
			base.Update();

			if (ThrowBackPack() && backpack.CanBeAimed)
			{
				backpack.InitializeState(BackpackStates.AIMING);
				isAimingBackpack = true;
				StartCoroutine(AimBackpack());
			}

			if (isAimingBackpack)
			{
				HandleBackpackAimingUI();
				aimingAnimation.DrawDottedLineAndArrow(pointA, pointB);
				aimingAnimation.IsAiming = true;
			}
			else
			{
				aimingAnimation.IsAiming = false;
			}
		}

		private void HandleMovement()
		{
			if (knockback.KnockbackCounter > 0) return;

			// Get Input
			float horizontal = Input.GetAxisRaw("Horizontal");
			float vertical = Input.GetAxisRaw("Vertical");

			// Clamp the magnitude so we cant move faster diagonally.
			Vector2 vel = new Vector2(horizontal, vertical);
			vel = Vector2.ClampMagnitude(vel, 1);

			base.Update();

			playerAnimations.SetIdleSprite(facingDirection);

			// Move the rigidbody from the BaseCharacterMovement
			Move(vel);
		}

		private void HandleBackpackAimingUI()
		{
			if (calculatedBackpackTravelDistance <= maxThrowDistance)
				aimingAnimation.UpdateUI(calculatedBackpackTravelDistance, Color.white);
			else
				aimingAnimation.UpdateUI(calculatedBackpackTravelDistance, Color.red);

			if (aimingAnimation.AimAngle > 90f || aimingAnimation.AimAngle < -90f)
				aimingAnimation.RotateText(-180f);
			else
				aimingAnimation.RotateText(0f);
		}

		public override void TakeDamage(float amount, Vector2 damageDirection)
		{
			this.RecalculateHealth(amount);
			onTakeDamage.Raise(healthStat);
			knockback.ApplyKnockback(damageDirection, damageColor);
		}

		public override void RecalculateHealth(float amount)
		{
			healthStat.runtimeStatValue -= amount;
		}

		private IEnumerator AimBackpack()
		{
			while (Input.GetKey(throwKey))
			{
				pointA = playerCenter.transform.position;
				pointB = cam.ScreenToWorldPoint(Input.mousePosition);

				if (Input.GetKeyDown(KeyCode.R))
				{
					backpack.InitializeState(BackpackStates.INHAND);
					isAimingBackpack = false;
					yield break;
				}

				calculatedBackpackTravelDistance = (pointB - pointA).sqrMagnitude;

				yield return new WaitForSeconds(Time.deltaTime);
			}

			// Throw the bag only if we have enough force points and strength.

			if (calculatedBackpackTravelDistance <= maxThrowDistance)
			{
				isAimingBackpack = false;
				onThrowEvent.Raise();
				backpack.Launch(pointB);
				backpack.InitializeState(BackpackStates.INFLIGHT);
				playerAnimations.TriggerThrowing(FacingDirection);
				yield break;
			}
			else
			{
				isAimingBackpack = false;
				backpack.InitializeState(BackpackStates.INHAND);
				yield break;
			}
		}

		private bool ThrowBackPack()
		{
			return Input.GetKeyDown(throwKey) && backpack.currentState == BackpackStates.INHAND;
		}

		public void Teleport(Vector2 pos)
		{
			rBody.position = pos;
            //playerStats.UseTeleport();
            FindObjectOfType<GhostingEffect>().ShowGhost();
            onTeleportEvent.Raise();
			trailRenderer.enabled = true;

			GameObject newTeleportEffect = Instantiate(teleportEffect, pos, teleportEffect.transform.rotation);
			Destroy(newTeleportEffect, 2f);

			AreaOfEffectDamage();

			// Update UI
			energyStat.runtimeStatValue = 0;
			onTeleportStatEvent.Raise(energyStat);

			Invoke("TurnTrailRendererOff", 0.5f);
		}

		private void TurnTrailRendererOff()
		{
			trailRenderer.enabled = false;
		}

		private void AreaOfEffectDamage()
		{
			Collider2D[] collidersInRadius = Physics2D.OverlapCircleAll(rBody.position, areaDamageRadius);

			if (collidersInRadius.Length > 0)
			{
				foreach (Collider2D col in collidersInRadius)
				{
					BaseEnemy enemy = col.GetComponent<BaseEnemy>();

					if (enemy != null)
					{
						Vector2 dir = col.transform.position - transform.position;
						enemy.GetComponent<IDamageable>().TakeDamage(areaDamageAmount, dir);
						GameObject effect = Instantiate(teleportEffect, rBody.position, Quaternion.identity);
						Debug.Log("Enemy damaged");
					}
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawWireSphere(transform.position, areaDamageRadius);
		}
	}
}


