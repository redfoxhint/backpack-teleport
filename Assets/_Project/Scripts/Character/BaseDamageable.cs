using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
		[SerializeField] protected ParticleSystem damageParticle;
		[SerializeField] protected EntityType entityType;

		// Private Variables
		protected float currentHealth;
		protected bool applyKnockbackAfterDamaged = true;

		// Properties
		public BaseCharacterData BaseCharacterData { set { baseCharacterData = value; } }
		public Color DamageColor { set { damageBlipColor = value; } }

		// Components
		[SerializeField] protected Image healthBar;
		protected Knockback knockback;
		protected SpriteRenderer spriteRenderer;

		protected virtual void Awake()
		{
			//healthBar = GetComponentInChildren<Image>();
			knockback = GetComponent<Knockback>();
			spriteRenderer = GetComponent<SpriteRenderer>();

			knockback.OnKnockbackFinished += OnKnockbackFinished;
		}

		private void OnDestroy()
		{
			knockback.OnKnockbackFinished -= OnKnockbackFinished;
		}

		protected virtual void Start()
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

		public virtual void RecalculateHealth(float amount)
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

		public virtual void AddHealth(float amount)
		{
			float newHealth = currentHealth + amount;
			currentHealth = newHealth;

			if(currentHealth > maxHealth)
			{
				currentHealth = maxHealth;
			}

			UpdateHealthbar(currentHealth);
		}

		public virtual void UpdateHealthbar(float newHealth)
		{
			if(healthBar != null)
			{
				healthBar.fillAmount = newHealth / maxHealth;
				Debug.Log($"New health ratio: {newHealth / maxHealth}");
			}
		}

		public virtual void IncreaseMaxHealth(float newMaxHealth)
		{
			maxHealth = newMaxHealth;
			UpdateHealthbar(currentHealth);
		}

		protected virtual void ApplyKnockback(Transform damageDealer)
		{
			knockback.ApplyKnockback(damageDealer);
			spriteRenderer.color = damageBlipColor;
		}

		protected virtual void OnKnockbackFinished()
		{
			spriteRenderer.color = Color.white;
		}

		protected virtual void Kill()
		{
			GameEvents.onEntityKilled.Invoke(entityType);
			Destroy(gameObject);
		}
	}
}


