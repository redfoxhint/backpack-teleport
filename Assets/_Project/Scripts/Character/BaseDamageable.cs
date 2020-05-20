using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;
using MyBox;
using System.Collections;
using System.Threading;
using UnityEngine.Experimental.GlobalIllumination;

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
		[SerializeField] private float incapacitationTime = 1f;

		[Space]

		[Header("Other Configuration")]
		[SerializeField] protected ParticleSystem damageParticle;
		[SerializeField] protected EntityType entityType;
		[SerializeField] protected bool destroyOnDeath = false;

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
		protected float currentIncapacitatedTime;
		protected bool isDead = false;
		protected bool isIncapacitated;
		private Vector2 currentKnockbackDirection;

		// Properties
		public bool Incapacitated { get => isIncapacitated; }

		// Components
		protected SpriteRenderer spriteRenderer;
		protected Animator animator;
		protected IWalkable walkable;

		// Events
		public Action onTookDamage;
		public Action onStunFinished;
		public Action onDeath;

		// Tasks
		private Task stunTask;

		#region Unity Functions

		protected virtual void Awake()
		{
			if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
			if (!animator) animator = GetComponent<Animator>(); ;
			if (walkable == null) walkable = GetComponent<IWalkable>();
		}
		protected virtual void Start()
		{
			SetHealth(maxHealth);
		}

		#endregion

		#region Public Functions

		public virtual void TakeDamage(Transform damageDealer, float amount)
		{
			// By default we just want to trigger the hurt animation and remove health.

			if (isDead) return;

			if(animator != null)
				animator.SetTrigger("hit");

			onTookDamage?.Invoke();
			DamageSequence(stunDuration);
			RemoveHealth(amount);

			if (isIncapacitated)
			{
				if(applyKnockback)
					ApplyKnockback(damageDealer, knockbackAmount);

				ResetStunDuration();
				damageParticle.Play();
				AudioManager.Instance.PlaySoundEffect(AudioFiles.SFX_SwordStab1, 0.5f);

				return;
			}

			if (applyKnockback)
				ApplyKnockback(damageDealer, knockbackAmount);

			if (applyStun)
				ApplyStun(stunDuration);

			AudioManager.Instance.PlaySoundEffect(AudioFiles.SFX_SwordStab1, 0.5f);
			damageParticle.Play();

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
			damageSequence.AppendCallback(() => animator.SetBool("isDamaged", true));
			damageSequence.AppendCallback(() => spriteRenderer.color = damageBlipColor);
			damageSequence.AppendInterval(duration);
			
			damageSequence.AppendCallback(() => animator.SetBool("isDamaged", false));
			damageSequence.AppendCallback(() => spriteRenderer.color = Color.white);
		}

		public virtual void Kill(bool invokeOnEntityKilled = true, bool waitForDeathAnimation = true)
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
			OnDeath();
			ResetStunTask();

			if(destroyOnDeath)
				Destroy(gameObject);
		}

		protected virtual void OnDeath()
		{

		}

		#region Knockback Functions

		private void ApplyKnockback(Transform source, float amount)
		{
			Rigidbody2D rBody = GetComponent<Rigidbody2D>();
			//rBody.velocity = Vector2.zero;

			Vector2 direction = source.position.DirectionTo(transform.position);
			currentKnockbackDirection = direction;
			rBody.AddForce(currentKnockbackDirection.normalized * amount, ForceMode2D.Impulse);
		}

        #endregion

        #region Stun Functions

		public void ApplyStun(float stunDuration)
		{
			if(!Incapacitated)
			{
				stunTask = new Task(StunRoutine());
				currentStunTime = stunDuration;
				isIncapacitated = true;

				// Play stun particles here

				if(stunnedParticleSystem != null)
					stunnedParticleSystem.Play();
			}
			else
			{
				ResetStunDuration();
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
			isIncapacitated = false;
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

		private void ResetStunDuration()
		{
			currentStunTime = stunDuration;
		}

        #endregion
    }
}


