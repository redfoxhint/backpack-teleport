﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum EntityType
{
	ENEMY_SPIDER,
	ENEMY_SPIDER_NEST,
	ENTITY_PLAYER
}

namespace BackpackTeleport.Character
{
	public class BaseDamageable : MonoBehaviour, IDamageable, IActivator
	{
		// Inspector Fields
		[Header("Damageable Character Configuration")]
		[SerializeField] protected float maxHealth; 
		[SerializeField] protected Color damageBlipColor = Color.red;
		[SerializeField] protected BaseCharacterData baseCharacterData;
		[SerializeField] protected ParticleSystem damageParticle;
		[SerializeField] protected EntityType entityType;

		// Private Variables
		protected float currentHealth;
		[SerializeField] protected bool applyKnockbackAfterDamaged = true;
		[SerializeField] private bool isDead = false;

		// Properties
		public BaseCharacterData BaseCharacterData { set { baseCharacterData = value; } }
		public Color DamageColor { set { damageBlipColor = value; } }

		// Components
		[SerializeField] protected Image healthBar;
		protected Knockback knockback;
		protected SpriteRenderer spriteRenderer;
		protected Animator animator;

		protected virtual void Awake()
		{
			knockback = GetComponent<Knockback>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();

			knockback.OnKnockbackFinished += OnKnockbackFinished;
		}

		private void OnDestroy()
		{
			knockback.OnKnockbackFinished -= OnKnockbackFinished;
		}

		protected virtual void Start()
		{
			currentHealth = maxHealth;
			UpdateStatBar(healthBar, currentHealth, maxHealth);
		}

		public virtual void TakeDamage(Transform damageDealer, float amount)
		{
			if (isDead) return;

			RemoveHealth(amount);
			animator.SetTrigger("hit");

			if (applyKnockbackAfterDamaged)
			{
				ApplyKnockback(damageDealer);
				damageParticle.Play();
			}
		}

		public virtual void RemoveHealth(float amount)
		{
			float newHealth = currentHealth - amount;

			currentHealth = newHealth;
			UpdateStatBar(healthBar, currentHealth, maxHealth);

			if (newHealth <= 0)
			{
				// Kill the character here
				Kill();
				return;
			}
		}

		public virtual void AddHealth(float amount)
		{
			float newHealth = currentHealth + amount;
			currentHealth = newHealth;

			if (currentHealth > maxHealth)
			{
				currentHealth = maxHealth;
			}

			UpdateStatBar(healthBar, currentHealth, maxHealth);
		}

		public virtual void UpdateStatBar(Image barToUpdate, float current, float max)
		{
			if(barToUpdate != null)
			{
				barToUpdate.fillAmount = current / max;
			}
		}

		public virtual void IncreaseMaxHealth(float newMaxHealth)
		{
			maxHealth = newMaxHealth;
			UpdateStatBar(healthBar, currentHealth, maxHealth);
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
			animator.SetTrigger("kill");
			isDead = true;
		}

		// Animation event
		protected void Die()
		{
			Destroy(gameObject);
		}
	}
}


