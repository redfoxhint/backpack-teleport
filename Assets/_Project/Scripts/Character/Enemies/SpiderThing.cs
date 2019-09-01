using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackpackTeleport.Character.PlayerCharacter;

namespace BackpackTeleport.Character.Enemy
{
	public class SpiderThing : BaseEnemy
	{
		[Header("Attack Configuations")]
		[SerializeField] private float attackCooldownTime = 1f;
		[SerializeField] private Image damageVisualization;

		private float currentCooldownTime;

		public override void InitAttackState()
		{
			base.InitAttackState();
			StartCoroutine(Attack());
		}

		public override void UpdateAttackState()
		{
			base.UpdateAttackState();

			if (currentCooldownTime <= Time.time)
			{
				StartCoroutine(Attack());
			}

		}

		public override void HandleMovementAnimation()
		{
			animator.SetFloat("Horizontal", Mathf.RoundToInt(agent2D.movingDirection.x));
			animator.SetFloat("Vertical", Mathf.RoundToInt(agent2D.movingDirection.y));
			animator.SetFloat("Speed", agent2D.movingDirection.sqrMagnitude);
		}

		IEnumerator Attack()
		{
			Vector2 attackDirection = transform.position - player.transform.position;
			//Debug.Log("Attacked");
			damageVisualization.gameObject.SetActive(true);
			currentCooldownTime = Time.time + attackCooldownTime;

			Collider2D[] collidersInRadius = Physics2D.OverlapCircleAll(rBody.position, 2f);

			if (collidersInRadius.Length > 0)
			{
				foreach (Collider2D col in collidersInRadius)
				{
					Player p = col.GetComponent<Player>();

					if (p != null)
					{
						Vector2 dir = col.transform.position - transform.position;
						p.GetComponent<IDamageable>().TakeDamage(0f, dir);
						//GameObject effect = Instantiate(teleportEffect, rBody.position, Quaternion.identity);
						Debug.Log("Player damaged");
					}
				}
			}

			yield return new WaitForSeconds(0.5f);
			damageVisualization.gameObject.SetActive(false);
			yield break;

		}
	}
}


