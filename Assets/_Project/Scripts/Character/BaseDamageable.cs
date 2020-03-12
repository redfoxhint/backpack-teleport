using System;
using UnityEngine;
using UnityEngine.UI;

public enum EntityType
{
	ENEMY_SPIDER,
	ENTITY_PLAYER
}

namespace BackpackTeleport.Character
{
	[RequireComponent(typeof(Knockback))]
	public class BaseDamageable : MonoBehaviour, IDamageable, IActivator
	{
		// Inspector Fields
		[Header("Damageable Character Configuration")]
		[SerializeField] protected float maxHealth = 10f; 
		[SerializeField] protected Color damageBlipColor = Color.red;
		[SerializeField] protected BaseCharacterData baseCharacterData;
		[SerializeField] private ParticleSystem damageParticle;
		[SerializeField] private EntityType entityType;

		// Private Variables
		protected float currentHealth;
		private bool applyKnockbackAfterDamaged = true;

		// Properties
		public BaseCharacterData BaseCharacterData { set { baseCharacterData = value; } }
		public Color DamageColor { set { damageBlipColor = value; } }

		// Components
		[SerializeField] protected Image healthBar;
		private Knockback knockback;
		private SpriteRenderer spriteRenderer;

		private void Awake()
		{
			healthBar = GetComponentInChildren<Image>();
			knockback = GetComponent<Knockback>();
			spriteRenderer = GetComponent<SpriteRenderer>();

			knockback.OnKnockbackFinished += OnKnockbackFinished;
		}

		private void OnDestroy()
		{
			knockback.OnKnockbackFinished -= OnKnockbackFinished;
		}

		private void Start()
		{
			currentHealth = maxHealth;
			UpdateHealthbar(currentHealth);
		}

		public virtual void TakeDamage(Transform damageDealer, float amount)
		{
			RecalculateHealth(amount);

			if (applyKnockbackAfterDamaged)
			{
				ApplyKnockback(damageDealer);
				damageParticle.Play();
			}
		}

		public void RecalculateHealth(float amount)
		{
			float newHealth = currentHealth - amount;

			if(newHealth <= 0)
			{
				// Kill the character here
				Kill();
				return;
			}

			currentHealth = newHealth;
			UpdateHealthbar(newHealth);
		}

		public void UpdateHealthbar(float newHealth)
		{
			if(healthBar != null)
			{
				healthBar.fillAmount = newHealth / maxHealth;
			}
		}

		public void IncreaseMaxHealth(float newMaxHealth)
		{
			maxHealth = newMaxHealth;
			UpdateHealthbar(currentHealth);
		}

		private void ApplyKnockback(Transform damageDealer)
		{
			knockback.ApplyKnockback(damageDealer);
			spriteRenderer.color = damageBlipColor;
		}

		private void OnKnockbackFinished()
		{
			spriteRenderer.color = Color.white;
		}

		private void Kill()
		{
			GameEvents.onEntityKilled.Invoke(entityType);
			Destroy(gameObject);
		}
	}
}


