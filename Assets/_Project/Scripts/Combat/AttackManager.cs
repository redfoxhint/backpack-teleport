using System.Collections;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [Header("Attack Configuration")]
    [SerializeField] private float attackRange;
    [SerializeField] private float damageAmount;
    [SerializeField] private float dashAttackDamageDelay;
    [SerializeField] private float knockbackAmount;
    [SerializeField] private LayerMask enemyFilter;

    [Header("Attack Locations")]
    [SerializeField] private Transform attackUpLocation;
    [SerializeField] private Transform attackDownLocation;
    [SerializeField] private Transform attackLeftLocation;
    [SerializeField] private Transform attackRightLocation;

    [SerializeField] private float nextAttackTime = 1; // The time between attacks before the combo is reset.
    [SerializeField] private float dashAmount;
    [SerializeField] private bool doDashAttack = true;
    private Animator animator;

    private float comboIndex = 0;
    private float currentComboTime = 0;
    private bool inCombo = false;

    public bool CanAttack { get; set; }

    // Components
    private PhysicsCharacterController characterController;
    private CharacterBase characterBase;


    private void Awake()
    {
        inCombo = false;
        CanAttack = true;
        animator = GetComponent<Animator>();
        characterController = GetComponent<PhysicsCharacterController>();
        characterBase = GetComponent<CharacterBase>();

        animator.SetFloat("ComboIndex", comboIndex);
    }

    private void Update()
    {
        if (inCombo)
        {
            if (currentComboTime > 0)
            {
                currentComboTime -= Time.deltaTime;
            }
            else
            {
                ResetCombo();
                currentComboTime = 0f;
            }
        }
    }

    public void Attack()
    {
        if (!CanAttack) return;
        characterController.DoMovement = false;

        // Deal damage here
        DealDamage();
        Debug.Log("Attacked");

        if (!inCombo)
        {
            StartCombo();
            return;
        }

        comboIndex += 1;

        if (comboIndex == 3)
        {
            SetAttack(comboIndex);
            ResetCombo();
            return;
        }

        SetAttack(comboIndex);
        currentComboTime = nextAttackTime;
    }

    private void DealDamage()
    {
        Transform attackLocation = CalculateAttackDirection();
        if (attackLocation == null) return;

        StartCoroutine(DealDelayedDamagedRoutine(attackLocation, dashAttackDamageDelay));
    }

    private IEnumerator DealDelayedDamagedRoutine(Transform damageColliderPos, float applyDamageDelay)
    {
        bool finished = false;

        while(!finished)
        {
            yield return new WaitForSeconds(applyDamageDelay);

            Collider2D[] detectedDamageables = Physics2D.OverlapCircleAll(damageColliderPos.position, attackRange, enemyFilter);

            if (detectedDamageables.Length > 0)
            {
                for (int i = 0; i < detectedDamageables.Length; i++)
                {
                    IDamageable damageable = detectedDamageables[i].GetComponent<IDamageable>();

                    if (damageable != null)
                    {
                        damageable.TakeDamage(this.gameObject.transform, damageAmount);
                    }
                }
            }

            finished = true;
            yield return null;
        }

        
    }

    private void StartCombo()
    {
        inCombo = true;
        comboIndex = 1;

        SetAttack(comboIndex);

        currentComboTime = nextAttackTime;
    }

    private void SetAttack(float index)
    {
        animator.SetFloat("ComboIndex", index);
        animator.SetTrigger("Attack");

        if (doDashAttack)
        {
            characterController.Dash();
        }
    }

    private void ResetCombo()
    {
        inCombo = false;
        currentComboTime = 0;
        comboIndex = 0;
    }

    private Transform CalculateAttackDirection()
    {
        float attackDirection = characterBase.FacingDirection;
        Transform attackLocation = null;

        switch (attackDirection)
        {
            case 0:
                attackLocation = attackDownLocation;
                break;

            case 1:
                attackLocation = attackRightLocation;
                break;

            case 2:
                attackLocation = attackLeftLocation;
                break;

            case 4:
                attackLocation = attackUpLocation;
                break;

            default:
                return null;
        }

        return attackLocation;
    }
}
