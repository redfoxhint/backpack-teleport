using System;
using UnityEngine;
using UnityEngine.UI;


namespace BackpackTeleport.Character
{
	[RequireComponent(typeof(Knockback))]
	public abstract class BaseCharacter : MonoBehaviour, IDamageable, IActivator
	{
		// Inspector Fields
		[SerializeField] protected float maxHealth = 10f;
		[SerializeField] protected Image healthBar;
		[SerializeField] protected BaseCharacterData baseCharacterData;
		[SerializeField] protected Color damageColor;

		// Private Variables
		protected float currentHealth;

		public BaseCharacterData BaseCharacterData { set { baseCharacterData = value; } }
		public Color DamageColor { set { damageColor = value; } }

		// Components
		protected Knockback knockback;

		protected virtual void Awake()
		{
			healthBar = GetComponentInChildren<Image>();
			knockback = GetComponent<Knockback>();
		}

		protected virtual void Start()
		{
			InitializeCharacter();
		}

		public virtual void InitializeCharacter()
		{
			currentHealth = maxHealth;
		}

		public virtual void TakeDamage(GameObject dealer, float amount)
		{
			RecalculateHealth(amount);

			if (currentHealth <= 0)
			{
				Destroy(gameObject); // Kill this character.
				return;
			}
		}

		public virtual void RecalculateHealth(float amount)
		{
			currentHealth -= amount;
			healthBar.fillAmount = currentHealth / maxHealth;
		}
	}
}


