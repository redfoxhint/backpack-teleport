using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;
using BackpackTeleport.Character.PlayerCharacter;

namespace BackpackTeleport.Character.Enemy
{
	[RequireComponent(typeof(PolyNavAgent))]
	public class BaseEnemy : BaseCharacter
	{
		// Inspector Fields
		[SerializeField] protected States currentState;

		// Components
		protected PolyNavAgent agent2D;
		protected Player player;

		public override void Awake()
		{
			base.Awake();
			agent2D = GetComponent<PolyNavAgent>();
			player = FindObjectOfType<Player>();
		}

		public override void Start()
		{
			base.Start();
			SetState(States.CHASE);
		}

		public override void Update()
		{
			UpdateStates(currentState);
			base.Update();
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

		private void UpdateStates(States state)
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
			//Debug.Log("Entered Attack State" + baseCharacterData.characterName);
		}

		public virtual void InitChaseState()
		{
			currentState = States.CHASE;
			//Debug.Log("Entered Chase State" + baseCharacterData.characterName);
		}

		public virtual void InitIdleState()
		{
			currentState = States.IDLE;
			//Debug.Log("Entered Idle State" + baseCharacterData.characterName);
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
			if (player != null)
			{
				Vector2 currentPosition = transform.position;
				Vector2 directionToPlayer = player.transform.position - transform.position;

				float distanceSqrToTarget = directionToPlayer.sqrMagnitude;

				if (distanceSqrToTarget <= agent2D.stoppingDistance + 1)
				{
					SetState(States.ATTACK);
					return;
				}

				if (player != null)
				{
					agent2D.SetDestination(player.transform.position);
					Vector2 target = agent2D.nextPoint;
					Vector2 direction = (target - (Vector2)transform.position).normalized;
					velocity = direction;
				}
				else
				{
					SetState(States.IDLE);
				}
			}
			else
			{
				SetState(States.IDLE);
			}
		}

		public virtual void UpdateIdleState()
		{
			Debug.Log("Updating enemy idle state...");
		}
	}
}



