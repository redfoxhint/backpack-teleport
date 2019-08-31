using UnityEngine;
using UnityEngine.UI;
using PolyNav;

namespace BackpackTeleport.Characters
{
	public abstract class BaseCharacter : BaseCharacterMovement, IDamageable
	{
		// Public Variables
		[SerializeField] protected float health = 10f;
		[SerializeField] protected Image healthBar;
		[SerializeField] protected States currentState;
		[SerializeField] protected BaseCharacterData baseCharacterData;

		// Private Variables
		protected float currentHealth;

		// Components
		protected PolyNavAgent agent2D;
		protected Player player;

		// Events
		[Header("Events")]
		[SerializeField] private StatEvent onStatChanged;

		// Tasks

		public override void Awake()
		{
			base.Awake();
			agent2D = GetComponent<PolyNavAgent>();
			player = FindObjectOfType<Player>();
		}

		public void Start()
		{
			InitializeCharacter();
			SetState(States.CHASE);
		}

		public override void Update()
		{
			UpdateStates(currentState);
			base.Update();
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
			Vector2 target = agent2D.nextPoint;
			Vector2 direction = (target - (Vector2)transform.position).normalized;
			velocity = direction;
		}


		

		public virtual void UpdateIdleState()
		{
			Debug.Log("Updating enemy idle state...");
		}
	}
}


