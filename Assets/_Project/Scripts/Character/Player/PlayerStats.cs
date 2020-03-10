using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
	// Public Variables
	[Header("Stats")]
	[SerializeField] private Stat healthStat;
	[SerializeField] private Stat energyStat;

	[Header("Stat Events")]
	[SerializeField] private StatEvent onTakeDamageEvent;
	[SerializeField] private StatEvent onTeleportEvent;

	private Knockback knockback;

	private void Awake()
	{
		knockback = GetComponent<Knockback>();
	}

	public void TakeDamage(Transform dealer, float amount)
	{
		healthStat.SetOldValue();
		healthStat.runtimeStatValue -= amount;

		onTakeDamageEvent.Raise(healthStat); // Raise an event pointing to the health player stat when the player takes damage.
		Debug.Log("Player took " + amount.ToString() + " damage.");
		//knockback.ApplyKnockback(dir, Color.red);
	}

	public void UseTeleport()
	{
		energyStat.runtimeStatValue = 0;
		onTeleportEvent.Raise(energyStat); // Raise an event pointing to the energy player stat when the player teleports.
		Debug.Log("Teleported");
	}
}
