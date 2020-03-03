using System.Collections;
using UnityEngine;
using BackpackTeleport.Character.Enemy;
using UnityEngine.InputSystem;

namespace BackpackTeleport.Character.PlayerCharacter
{
	[RequireComponent(typeof(AimingAnimation))]
	public class Player : BaseCharacter
	{
		// Public Fields
		[SerializeField] private Transform playerCenter;
		public Transform PlayerCenter { get => playerCenter; }

		[SerializeField] private GameObject teleportEffect;

		// Properties
		public Rigidbody2D RBody2D { get; private set; }
		
		public PlayerAnimations PlayerAnimations { get => playerAnimations; }

		// Components
		private PlayerAnimations playerAnimations;
		private PlayerMovementController movement;
		private AimingAnimation aimingAnimation;
		private AttackManager attackManager;
		private InputManager inputManager;
		private DottedLine dottedLine;
		private TrailRenderer trailRenderer;
		private Animator animator;
		private Camera cam;

		protected override void Awake()
		{
			base.Awake();

			animator = GetComponent<Animator>();
			movement = GetComponent<PlayerMovementController>();
			aimingAnimation = GetComponent<AimingAnimation>();
			playerAnimations = GetComponent<PlayerAnimations>();
			attackManager = GetComponent<AttackManager>();
			trailRenderer = GetComponent<TrailRenderer>();
			RBody2D = GetComponent<Rigidbody2D>();

			dottedLine = DottedLine.Instance;
			cam = Camera.main;
			inputManager = InputManager.Instance;

			inputManager.InputActions.Player.BasicAttack.performed += Attack;
		}

		protected override void Start()
		{
			base.Start();

			trailRenderer.enabled = false;
			GameEvents.onBackpackThrownEvent.AddListener(OnBackpackThrown);
		}

		private void OnDisable()
		{
			GameEvents.onBackpackThrownEvent.RemoveAllListeners();
		}

		private void Update()
		{
			
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

			GameEvents.onTeleportedEvent.Invoke(pos);

			Invoke("TurnTrailRendererOff", 0.5f);
		}

		private void TurnTrailRendererOff()
		{
			trailRenderer.enabled = false;
		}

		private void Attack(InputAction.CallbackContext value)
		{
			attackManager.Attack();
		}
	}
}


// For aoe damage.
// TODO: Move this to its own class or in the attack manager.

//[Header("Area of Effect Damage Settings")]
//[Space]
//[SerializeField] private float areaDamageAmount = 3f;
//[SerializeField] private float areaDamageRadius = 4f;


//private void AreaOfEffectDamage()
//{
//	Collider2D[] collidersInRadius = Physics2D.OverlapCircleAll(transform.position, areaDamageRadius);

//	if (collidersInRadius.Length > 0)
//	{
//		foreach (Collider2D col in collidersInRadius)
//		{
//			BaseEnemy enemy = col.GetComponent<BaseEnemy>();

//			if (enemy != null)
//			{
//				Vector2 dir = col.transform.position - transform.position;
//				enemy.GetComponent<IDamageable>().TakeDamage(gameObject, areaDamageAmount);
//				GameObject effect = Instantiate(teleportEffect, transform.position, Quaternion.identity);
//				Debug.Log("Enemy damaged");
//			}
//		}
//	}
//}

