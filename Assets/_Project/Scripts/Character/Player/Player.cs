using System.Collections;
using UnityEngine;
using BackpackTeleport.Character.Enemy;
using UnityEngine.InputSystem;

namespace BackpackTeleport.Character.PlayerCharacter
{
	[RequireComponent(typeof(AimingAnimation))]
	public class Player : MonoBehaviour
	{
		// Public Fields
		[SerializeField] private Transform playerCenter;
		public Transform PlayerCenter { get => playerCenter; }

		[SerializeField] private GameObject teleportEffect;

		// Properties
		public Rigidbody2D RBody2D { get; private set; }

		// Components
		private AttackManager attackManager;
		private InputManager inputManager;
		private TrailRenderer trailRenderer;
		private Animator animator;
		private Camera cam;

		private void Awake()
		{
			animator = GetComponent<Animator>();
			attackManager = GetComponent<AttackManager>();
			trailRenderer = GetComponent<TrailRenderer>();
			RBody2D = GetComponent<Rigidbody2D>();

			cam = Camera.main;
			inputManager = InputManager.Instance;

			inputManager.InputActions.Player.BasicAttack.started += Attack;
		}

		private void Start()
		{
			trailRenderer.enabled = false;
		}

		private void OnDisable()
		{
			GameEvents.onBackpackThrownEvent.RemoveAllListeners();
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

