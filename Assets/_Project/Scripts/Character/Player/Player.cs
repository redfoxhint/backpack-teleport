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
		public Transform PlayerCenter { get => playerCenter; }
		public PlayerAnimations PlayerAnimations { get => playerAnimations; }

		// Components
		private Backpack backpack;
		private PlayerAnimations playerAnimations;
		private PlayerMovementController movement;
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
			movement = GetComponent<PlayerMovementController>();
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
			GameEvents.onBackpackThrownEvent.AddListener(OnBackpackThrown);
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				Attack();
			}
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

		// Gets raised from the Aiming state of the backpack
		private void OnBackpackThrown(Backpack backpack)
		{
			playerAnimations.TriggerThrowing(movement.FacingDirection);
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


