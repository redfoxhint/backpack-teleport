using UnityEngine;
using UnityEngine.UI;
using PolyNav;

namespace BackpackTeleport.Characters
{
	public abstract class BaseCharacter : MonoBehaviour, IDamageable
	{
		// Public Variables
		[SerializeField] protected float health = 10f;
		[SerializeField] protected float moveSpeed = 3f;
		[SerializeField] protected Image healthBar;
		[SerializeField] protected States currentState;
		[SerializeField] protected BaseCharacterData baseCharacterData;

		// Private Variables
		protected float currentHealth;

		// Components
		protected Animator animator;
		protected PolyNavAgent agent2D;
		protected Player player;

		// Events
		[Header("Events")]
		[SerializeField] private StatEvent onStatChanged;

		// Tasks

		private void Awake()
		{
			animator = GetComponent<Animator>();
			agent2D = GetComponent<PolyNavAgent>();
			player = FindObjectOfType<Player>();
		}

		private void Start()
		{
			InitializeCharacter();
			SetState(States.CHASE);
		}

		private void Update()
		{
			UpdateStates(currentState);
		}

		private void InitializeCharacter()
		{
			currentHealth = health;
		}

		public void TakeDamage(float amount)
		{
			RecalculateHealth(amount);

			if (currentHealth <= 0)
			{
				Destroy(gameObject); // Kill this character.
				return;
			}
		}

		private void Movement()
		{
			//agent2D.destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		private void RecalculateHealth(float amount)
		{
			currentHealth -= amount;
			healthBar.fillAmount = currentHealth / health;
		}

		public void SetState(States state)
		{
			switch (state)
			{
				case States.ATTACK:
					InitAttackState();
					break;
				case States.CHASE:
					InitChaseState();
					break;
				case States.IDLE:
					InitIdleState();
					break;
			}
		}

		public void UpdateStates(States state)
		{
			switch (state)
			{
				case States.ATTACK:
					UpdateAttackState();
					break;
				case States.CHASE:
					UpdateChaseState();
					break;
				case States.IDLE:
					UpdateIdleState();
					break;
			}
		}

		public virtual void InitAttackState()
		{
			currentState = States.ATTACK;
			Debug.Log("Entered Attack State" + baseCharacterData.characterName);
		}

		public virtual void InitChaseState()
		{
			currentState = States.CHASE;
			Debug.Log("Entered Chase State" + baseCharacterData.characterName);
		}

		public virtual void InitIdleState()
		{
			currentState = States.IDLE;
			Debug.Log("Entered Idle State" + baseCharacterData.characterName);
		}

		public virtual void UpdateAttackState()
		{
			Vector2 currentPosition = transform.position;
			Vector2 directionToPlayer = player.transform.position - transform.position;

			float distanceSqrToTarget = directionToPlayer.sqrMagnitude;

			if (distanceSqrToTarget > agent2D.stoppingDistance + 1)
			{
				SetState(States.CHASE);
			}
		}

		public virtual void UpdateChaseState()
		{
			Vector2 currentPosition = transform.position;
			Vector2 directionToPlayer = player.transform.position - transform.position;

			float distanceSqrToTarget = directionToPlayer.sqrMagnitude;

			if (distanceSqrToTarget <= agent2D.stoppingDistance + 1)
			{
				SetState(States.ATTACK);
				return;
			}

			agent2D.SetDestination(player.transform.position);
		}

		public virtual void UpdateIdleState()
		{
			Debug.Log("Updating enemy idle state...");
		}
	}
}


