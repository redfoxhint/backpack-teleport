using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;
using MyBox;
using System.Collections;

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
		[Header("Health Configuration")]
		[SerializeField] [Min(5)] protected float maxHealth = 10;
		[SerializeField] protected Image healthBar;

		[Space]

		[Header("Damage Configuration")]
		
		[SerializeField] protected Color damageBlipColor = Color.red;

		[Space]

		[Header("Other Configuration")]
		[SerializeField] protected ParticleSystem damageParticle;
		[SerializeField] protected EntityType entityType;

		[Space]

		[Header("Knockback Configuration")]
		[SerializeField] protected bool applyKnockback = true;
		[SerializeField] protected float knockbackAmount = 35f;
		[SerializeField] protected float knockbackDuration = 1f;

		[Header("Stun Configuration")]
		[SerializeField] protected bool applyStun = true;
		[SerializeField] protected float stunDuration = 2f;
		[SerializeField] private ParticleSystem stunnedParticleSystem;

		// Private Variables
		protected float currentHealth;
		protected float currentStunTime;
		protected bool isDead = false;

		// Properties
		public bool IsStunned
		{
			get
			{
				if (stunTask != null)
				{
					if (!stunTask.Running)
						return false;

					return true;
				}

				return false;
			}
		}

		// Components
		protected Knockback knockback;
		protected SpriteRenderer spriteRenderer;
		protected Animator animator;
		protected IWalkable walkable;

		// Events
		public Action onTookDamage;
		public Action onDamageFinished;
		public Action onStunFinished;
		public Action onDeath;

		// Tasks
		private Task stunTask;

		#region Unity Functions

		protected virtual void Awake()
		{
			if (!knockback) knockback = GetComponent<Knockback>();
			if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
			if (!animator) animator = GetComponent<Animator>(); ;
			if (walkable == null) walkable = GetComponent<IWalkable>();

			Debug.Log(walkable);
		}
		protected virtual void Start()
		{
			InitKnockback();

			SetHealth(maxHealth);
			//UpdateStatBar(healthBar, currentHealth, maxHealth);
		}

		#endregion

		#region Public Functions

		public virtual void TakeDamage(Transform damageDealer, float amount)
		{
			if (isDead) return;

			if(animator != null)
				animator.SetTrigger("hit");

			DamageSequence(knockbackDuration);

			if (applyKnockback)
			{
				ApplyKnockback(damageDealer, knockbackDuration, knockbackAmount);

				if (animator != null)
					damageParticle.Play();
			}

			if(applyStun)
			{
				ApplyStun(stunDuration);
			}

			onTookDamage?.Invoke(); // Invoke damage event
			AudioManager.Instance.PlaySoundEffect(AudioFiles.SFX_SwordStab1, 0.5f);
			RemoveHealth(amount);
		}

		public void SetHealth(float value)
		{
			if (value == currentHealth) return;

			if(value > currentHealth)
			{
				AddHealth(value);
			}
			else if(value < currentHealth)
			{
				RemoveHealth(value);
			}
			else if(value <= 0)
			{
				Kill();
			}
		}

		public virtual void SetMaxHealth(float value)
		{
			if (value <= 0) return; // Cancel if the value is 0

			maxHealth = value;
			UpdateStatBar(healthBar, currentHealth, maxHealth);
		}

		public virtual void RemoveHealth(float value)
		{
			float newHealth = currentHealth - value;

			currentHealth = newHealth;
			UpdateStatBar(healthBar, currentHealth, maxHealth);

			if (newHealth <= 0)
			{
				// Kill the character here
				Kill();
				return;
			}
		}

		public virtual void AddHealth(float value)
		{
			float newHealth = currentHealth + value;
			currentHealth = newHealth;

			if (currentHealth > maxHealth)
			{
				currentHealth = maxHealth;
			}

			UpdateStatBar(healthBar, currentHealth, maxHealth);
		}

		#endregion

		#region Private Functions
		protected virtual void UpdateStatBar(Image barToUpdate, float current, float max)
		{
			if (barToUpdate != null)
			{
				barToUpdate.fillAmount = current / max;
			}
		}

		#endregion
		
		protected virtual void DamageSequence(float duration)
		{
			Sequence damageSequence = DOTween.Sequence();
			damageSequence.AppendCallback(() => walkable.ToggleMovement(false));
			damageSequence.AppendCallback(() => animator.SetBool("isDamaged", true));
			damageSequence.AppendCallback(() => spriteRenderer.color = damageBlipColor);
			damageSequence.AppendInterval(duration);
			
			damageSequence.AppendCallback(() => animator.SetBool("isDamaged", false));
			damageSequence.AppendCallback(() => spriteRenderer.color = Color.white);

			if(!applyStun)
			{
				damageSequence.AppendCallback(() => walkable.ToggleMovement(true));
			}
			
			damageSequence.OnComplete(() => onDamageFinished?.Invoke());
		}

		protected virtual void Kill(bool invokeOnEntityKilled = true, bool waitForDeathAnimation = true)
		{
			if(invokeOnEntityKilled)
			{
				GameEvents.onEntityKilled.Invoke(entityType);
			}
			
			if(waitForDeathAnimation)
			{
				animator.SetTrigger("kill");
				OnEndofDeathAnimation();
			}

			isDead = true;
		}

		// Animation event
		protected void OnEndofDeathAnimation()
		{
			onDeath?.Invoke();
			ResetStunTask();
			Destroy(gameObject);
		}

		#region Knockback Functions

		private void InitKnockback()
		{
			if(knockback != null)
			{
				knockback.OnKnockbackStarted += OnKnockbackStarted;
				knockback.OnKnockbackFinished += OnKnockbackFinished;
			}
		}

		protected virtual void ApplyKnockback(Transform source, float duration, float amount)
		{
			knockback.ApplyKnockback(source, duration, amount);
		}

		private void OnKnockbackStarted()
		{
			//onTookDamage?.Invoke();
		}

		private void OnKnockbackFinished()
		{
			onDamageFinished?.Invoke();
		}

        #endregion

        #region Stun Functions

		public void ApplyStun(float stunDuration)
		{
			if(!IsStunned)
			{
				stunTask = new Task(StunRoutine());
				currentStunTime = stunDuration;

				// Play stun particles here

				if(stunnedParticleSystem != null)
					stunnedParticleSystem.Play();
			}
			else
			{
				currentStunTime = stunDuration;
			}
		}

		private IEnumerator StunRoutine()
		{
			while(currentStunTime > 0f)
			{
				currentStunTime -= Time.deltaTime;
				yield return null;
			}

			walkable.ToggleMovement(true);

			// Stop stun particles here
			if (stunnedParticleSystem != null)
				stunnedParticleSystem.Stop();

			onStunFinished?.Invoke();
			yield break;
		}

		private void ResetStunTask()
		{
			if(stunTask != null && stunTask.Running)
			{
				stunTask.Stop();
				stunTask = null;
			}
		}

        #endregion
    }
}


