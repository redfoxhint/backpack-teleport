using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BackpackTeleport.Characters
{
	public class Slime : BaseCharacter
	{
		[Header("Attack Configuations")]
		[SerializeField] private float attackCooldownTime = 1f;
		[SerializeField] private Image damageVisualization;
		[SerializeField] private NavMeshObstacle2D obstacle2D;

		private float currentCooldownTime;

		public override void InitAttackState()
		{
			base.InitAttackState();
			StartCoroutine(Attack());
		}

		public override void UpdateAttackState()
		{
			base.UpdateAttackState();

			if(currentCooldownTime <= Time.time)
			{
				StartCoroutine(Attack());
			}

		}

		IEnumerator Attack()
		{
			Vector2 attackDirection = transform.position - player.transform.position;
			Debug.Log("Attacked");
			damageVisualization.gameObject.SetActive(true);
			currentCooldownTime = Time.time + attackCooldownTime;
			yield return new WaitForSeconds(0.5f);
			damageVisualization.gameObject.SetActive(false);
			yield break;
			
		}
	}
}


