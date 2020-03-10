using System;
using UnityEngine;
using UnityEngine.UI;


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
			}
		}

		public void RecalculateHealth(float amount)
		{
			float newHealth = currentHealth - amount;

			if(newHealth <= 0)
			{
				// Kill the character here
				Destroy(gameObject);
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
	}
}


