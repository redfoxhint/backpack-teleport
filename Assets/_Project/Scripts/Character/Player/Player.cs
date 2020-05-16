using System.Collections;
using UnityEngine;
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
			GameEvents.onBackpackThrownEvent.AddListener(TriggerThrowing);
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
			AudioManager.Instance.PlaySoundEffect(AudioFiles.SFX_Teleport1);
		}

		private void TurnTrailRendererOff()
		{
			trailRenderer.enabled = false;
		}

		private void Attack(InputAction.CallbackContext value)
		{
			attackManager.Attack();
		}

		public void SwitchHasBackback(bool hasBackpack)
		{
			animator.SetBool("hasBackpack", true);
		}

		public void TriggerThrowing()
		{
			animator.SetTrigger("Throw");
			AudioManager.Instance.PlaySoundEffect(AudioFiles.SFX_Teleport2);
		}
	}
}

