using System.Collections;
using UnityEngine;
using BackpackTeleport.Character.Enemy;

namespace BackpackTeleport.Character.PlayerCharacter
{
	[RequireComponent(typeof(AimingAnimation), typeof(PlayerMovement))]
	public class Player : BaseCharacter
	{
		// Inspector Fields
		[Header("Backpack Settings")]
		[Space]
		[SerializeField] private float maxThrowDistance = 300f;
		[SerializeField] private KeyCode throwKey = KeyCode.Mouse1;

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

		// Private Variables
		private Vector2 pointA;
		private Vector2 pointB;

		private bool isAimingBackpack;
		private float calculatedBackpackTravelDistance;

		// Properties
		public Rigidbody2D RBody2D { get; private set; }

		// Components
		private Backpack backpack;
		private PlayerAnimations playerAnimations;
		private PlayerMovement characterMovement;
		private AimingAnimation aimingAnimation;
		private MeleeAttack attackManager;
		private DottedLine dottedLine;
		private TrailRenderer trailRenderer;
		private Animator animator;
		private Camera cam;

		protected override void Awake()
		{
			base.Awake();

			backpack = FindObjectOfType<Backpack>();
			animator = GetComponent<Animator>();
			characterMovement = GetComponent<PlayerMovement>();
			aimingAnimation = GetComponent<AimingAnimation>();
			playerAnimations = GetComponent<PlayerAnimations>();
			attackManager = GetComponent<MeleeAttack>();
			trailRenderer = GetComponent<TrailRenderer>();
			RBody2D = GetComponent<Rigidbody2D>();

			dottedLine = DottedLine.Instance;
			cam = Camera.main;
		}

		protected override void Start()
		{
			base.Start();

			isAimingBackpack = false;
			trailRenderer.enabled = false;

			TestNonMonoUpdate testMono = new TestNonMonoUpdate();
		}

		private void Update()
		{
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

			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				Attack();
			}
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

		public override void TakeDamage(GameObject dealer, float amount)
		{
			this.RecalculateHealth(amount);
			onTakeDamage.Raise(healthStat);

			characterMovement.ApplyKnockback(dealer.transform, 10f);

			//knockback.ApplyKnockback(damageDirection, damageColor);
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
				playerAnimations.TriggerThrowing(characterMovement.FacingDirection);
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
			transform.position = pos;
            //FindObjectOfType<GhostingEffect>().ShowGhost();
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
			Collider2D[] collidersInRadius = Physics2D.OverlapCircleAll(transform.position, areaDamageRadius);

			if (collidersInRadius.Length > 0)
			{
				foreach (Collider2D col in collidersInRadius)
				{
					BaseEnemy enemy = col.GetComponent<BaseEnemy>();

					if (enemy != null)
					{
						Vector2 dir = col.transform.position - transform.position;
						enemy.GetComponent<IDamageable>().TakeDamage(gameObject, areaDamageAmount);
						GameObject effect = Instantiate(teleportEffect, transform.position, Quaternion.identity);
						Debug.Log("Enemy damaged");
					}
				}
			}
		}

		private void Attack()
		{
			attackManager.Attack(this, animator, characterMovement.LastVelocity);
			CustomTestEventData data = new CustomTestEventData(transform);
			GameEvents.testEvent.Invoke(data);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawWireSphere(transform.position, areaDamageRadius);
		}
	}
}


