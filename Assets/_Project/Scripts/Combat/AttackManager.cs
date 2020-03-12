using BackpackTeleport.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public struct AttackData
    {
        public float damageDealt; // Not implemented. Meant for getting how much damage the attack would deal. The damage amount calculation would probably happen in a seperate damage class to make damage reuseable.
        public float dashKnockbackAmount; // For storing how much the last attack knocked back the enemy. This cna also be randomized in a seperate knockback class.
        public List<BaseDamageable> baseDamageablesDetectedInLastHit; // Base Damageables detected from last attack.

        public float lastAttackDirection; // The last floating point direction we last attacked in.
        public Transform lastAttackLocation; // The last "collider direction" that was used to detect damageables;

        public AttackData(float _damageDealt, float _dashKnockbackAmount, List<BaseDamageable> _baseDamageablesDetected, Transform _lastAttackLocation, float _lastAttackDirection)
        {
            damageDealt = _damageDealt;
            dashKnockbackAmount = _dashKnockbackAmount;
            baseDamageablesDetectedInLastHit = _baseDamageablesDetected;
            lastAttackLocation = _lastAttackLocation;
            lastAttackDirection = _lastAttackDirection;
        }
    }

    [Header("Attack Configuration")]
    [SerializeField] private float attackRange;
    [SerializeField] private float damageAmount;
    [SerializeField] private float dashAttackDamageDelay;
    [SerializeField] private float knockbackAmount;
    [SerializeField] private LayerMask enemyFilter;
    [SerializeField] private bool debug;
    [SerializeField] private float attackAnimationTime = 1; // How long the attack lasts

    [Header("Particles")]
    [SerializeField] private ParticleSystem attackParticleUp;
    [SerializeField] private ParticleSystem attackParticleDown;
    [SerializeField] private ParticleSystem attackParticleRight;
    [SerializeField] private ParticleSystem attackParticleLeft;

    [Header("Attack Locations")]
    [SerializeField] private Transform attackUpLocation;
    [SerializeField] private Transform attackDownLocation;
    [SerializeField] private Transform attackLeftLocation;
    [SerializeField] private Transform attackRightLocation;

    [SerializeField] private float comboResetTime = 1; // The time between attacks before the combo is reset.
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
    private Camera cam;
    private float nextAttackDirection;

    private void Awake()
    {
        inCombo = false;
        CanAttack = true;
        animator = GetComponent<Animator>();
        characterController = GetComponent<PhysicsCharacterController>();
        characterBase = GetComponent<CharacterBase>();
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
        else
        {
            SetAttack(comboIndex);
            currentComboTime = comboResetTime;
        }
        
        Utils.ShakeCameraPosition(Camera.main.transform, 0.6f, 0.08f, 15, 0f, false);
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
        ParticleSystem attackParticle = GetAttackParticle(nextAttackDirection);
        attackParticle.Play();

        while (!finished)
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

        currentComboTime = comboResetTime;
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
        animator.SetFloat("ComboIndex", 0f);
    }

    private Transform CalculateAttackDirection()
    {
        float attackDirection = characterBase.FacingDirection;
        nextAttackDirection = attackDirection;
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

    private ParticleSystem GetAttackParticle(float attackDirection)
    {
        switch(attackDirection)
        {
            case 0:
                return attackParticleDown;

            case 1:
                return attackParticleRight;

            case 2:
                return attackParticleLeft;

            case 4:
                return attackParticleUp;

            default:
                return null;
        }
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying && debug)
        {
            // Attack location

            Gizmos.color = Color.red;
            Transform circleLocation = CalculateAttackDirection();

            if (circleLocation == null) return;

            Gizmos.DrawSphere(circleLocation.position, attackRange);

            // Attack Range
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(circleLocation.position, attackRange);
        }
    }
}


