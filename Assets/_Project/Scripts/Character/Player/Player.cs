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
		[SerializeField] private KeyCode throwKey = KeyCode.Mouse1;
		[SerializeField] private Transform playerCenter;
		[SerializeField] private GameObject teleportEffect;

		[Header("Area of Effect Damage Settings")]
		[Space]
		[SerializeField] private float areaDamageAmount = 3f;
		[SerializeField] private float areaDamageRadius = 4f;

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
		private CharacterController2D cc2D;
		private AimingAnimation aimingAnimation;
		private AttackManager attackManager;
		private DottedLine dottedLine;
		private TrailRenderer trailRenderer;
		private Animator animator;
		private Camera cam;

		protected override void Awake()
		{
			base.Awake();

			backpack = FindObjectOfType<Backpack>();
			animator = GetComponent<Animator>();
			cc2D = GetComponent<CharacterController2D>();
			aimingAnimation = GetComponent<AimingAnimation>();
			playerAnimations = GetComponent<PlayerAnimations>();
			attackManager = GetComponent<AttackManager>();
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
		}

		public override void RecalculateHealth(float amount)
		{
			// TODO: Implement health system
			//healthStat.runtimeStatValue -= amount;
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
				backpack.Launch(pointB);
				backpack.InitializeState(BackpackStates.INFLIGHT);
				playerAnimations.TriggerThrowing(cc2D.FacingDirection);
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
			trailRenderer.enabled = true;

			GameObject newTeleportEffect = Instantiate(teleportEffect, pos, teleportEffect.transform.rotation);
			Destroy(newTeleportEffect, 2f);

			AreaOfEffectDamage();

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
			attackManager.Attack();
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawWireSphere(transform.position, areaDamageRadius);
		}
	}
}


