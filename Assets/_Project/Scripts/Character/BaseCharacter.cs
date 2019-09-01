using UnityEngine;
using UnityEngine.UI;


namespace BackpackTeleport.Character
{
	[RequireComponent(typeof(Knockback))]
	public abstract class BaseCharacter : BaseCharacterMovement, IDamageable
	{
		// Inspector Fields
		[SerializeField] protected float maxHealth = 10f;
		[SerializeField] protected Image healthBar;
		[SerializeField] protected BaseCharacterData baseCharacterData;
		[SerializeField] protected Color damageColor;

		// Private Variables
		protected float currentHealth;

		// Components
		protected Knockback knockback;

		public override void Awake()
		{
			base.Awake();
			knockback = GetComponent<Knockback>();
		}

		public virtual void Start()
		{
			InitializeCharacter();
		}

		public override void Update()
		{
			base.Update();
		}

		public virtual void InitializeCharacter()
		{
			currentHealth = maxHealth;
		}

		public virtual void TakeDamage(float amount, Vector2 damageDirection)
		{
			RecalculateHealth(amount);

			if (currentHealth <= 0)
			{
				Destroy(gameObject); // Kill this character.
				return;
			}

			knockback.ApplyKnockback(damageDirection, damageColor);
		}

		public virtual void RecalculateHealth(float amount)
		{
			currentHealth -= amount;
			healthBar.fillAmount = currentHealth / maxHealth;
		}
	}
}


