using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;
using BackpackTeleport.Character.PlayerCharacter;

namespace BackpackTeleport.Character.Enemy
{
	[RequireComponent(typeof(PolyNavAgent))]
	public class BaseEnemy : BaseDamageable
	{
		//	// Inspector Fields
		//	[SerializeField] protected States currentState;
		//	[SerializeField] private bool doMove;

		//	// Components
		//	protected CharacterMovement characterMovement;
		//	protected PolyNavAgent agent2D;
		//	protected Animator animator;
		//	protected Player player;

		//	protected override void Awake()
		//	{
		//		base.Awake();
		//		animator = GetComponent<Animator>();
		//		characterMovement = GetComponent<CharacterMovement>();
		//		agent2D = GetComponent<PolyNavAgent>();
		//		player = FindObjectOfType<Player>();
		//	}

		//	protected override void Start()
		//	{
		//		base.Start();
		//		SetState(States.CHASE);
		//	}

		//	private void Update()
		//	{
		//		if (!doMove) return;

		//		UpdateStates(currentState);
		//		HandleMovementAnimation();
		//	}

		//	public void SetState(States state)
		//	{
		//		switch (state)
		//		{
		//			case States.ATTACK:
		//				InitAttackState();
		//				break;
		//			case States.CHASE:
		//				InitChaseState();
		//				break;
		//			case States.IDLE:
		//				InitIdleState();
		//				break;
		//		}
		//	}

		//	private void UpdateStates(States state)
		//	{
		//		switch (state)
		//		{
		//			case States.ATTACK:
		//				UpdateAttackState();
		//				break;
		//			case States.CHASE:
		//				UpdateChaseState();
		//				break;
		//			case States.IDLE:
		//				UpdateIdleState();
		//				break;
		//		}
		//	}

		//	public override void TakeDamage(Transform dealer, float amount)
		//	{
		//		base.TakeDamage(dealer, amount);
		//		characterMovement.ApplyKnockback(dealer.transform, 1f);
		//	}

		//	private void HandleMovementAnimation()
		//	{
		//		animator.SetFloat("Horizontal", Mathf.RoundToInt(agent2D.movingDirection.x));
		//		animator.SetFloat("Vertical", Mathf.RoundToInt(agent2D.movingDirection.y));
		//		animator.SetFloat("Speed", agent2D.movingDirection.sqrMagnitude);
		//	}

		//	public virtual void InitAttackState()
		//	{
		//		currentState = States.ATTACK;
		//		//Debug.Log("Entered Attack State" + baseCharacterData.characterName);
		//	}

		//	public virtual void InitChaseState()
		//	{
		//		currentState = States.CHASE;
		//		//Debug.Log("Entered Chase State" + baseCharacterData.characterName);
		//	}

		//	public virtual void InitIdleState()
		//	{
		//		currentState = States.IDLE;
		//		//Debug.Log("Entered Idle State" + baseCharacterData.characterName);
		//	}

		//	public virtual void UpdateAttackState()
		//	{
		//		Vector2 currentPosition = transform.position;
		//		Vector2 directionToPlayer = player.transform.position - transform.position;

		//		float distanceSqrToTarget = directionToPlayer.sqrMagnitude;

		//		if (distanceSqrToTarget > agent2D.stoppingDistance + 1)
		//		{
		//			SetState(States.CHASE);
		//		}
		//	}

		//	public virtual void UpdateChaseState()
		//	{
		//		if (player != null)
		//		{
		//			Vector2 currentPosition = transform.position;
		//			Vector2 directionToPlayer = player.transform.position - transform.position;

		//			float distanceSqrToTarget = directionToPlayer.sqrMagnitude;

		//			if (distanceSqrToTarget <= agent2D.stoppingDistance + 1)
		//			{
		//				SetState(States.ATTACK);
		//				return;
		//			}

		//			if (player != null)
		//			{
		//				agent2D.SetDestination(player.transform.position);
		//				Vector2 target = agent2D.nextPoint;
		//				Vector2 direction = (target - (Vector2)transform.position).normalized;
		//				//velocity = direction;
		//			}
		//			else
		//			{
		//				SetState(States.IDLE);
		//			}
		//		}
		//		else
		//		{
		//			SetState(States.IDLE);
		//		}
		//	}

		//	public virtual void UpdateIdleState()
		//	{
		//		Debug.Log("Updating enemy idle state...");
		//	}
		//}
	}
}



