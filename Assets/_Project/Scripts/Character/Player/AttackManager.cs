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

    [System.Serializable]
    public struct AttackDirectionData
    {
        public Vector2 attackDirectionColliderSize;
        public Transform attackLocation;
        public ParticleSystem attackParticle;
        public float colliderAngle;
    }

    [Header("Attack Configuration")]
    [SerializeField] private bool debug;
    [SerializeField] private float attackRange;
    [SerializeField] private float damageAmount;
    [SerializeField] private float dashAttackDamageDelay;
    [SerializeField] private float knockbackAmount;
    [SerializeField] private float attackAnimationTime = 1; // How long the attack lasts
    [SerializeField] private LayerMask enemyFilter;
    [SerializeField] private AudioClip attackAudio;

    [Header("Attack Data")]
    [SerializeField] private List<AttackDirectionData> attackDirectionData;

    [SerializeField] private float comboResetTime = 1; // The time between attacks before the combo is reset.
    [SerializeField] private float dashAmount;
    [SerializeField] private bool doDashAttack = true;
    private Animator animator;

    private float comboIndex = 0;
    private float currentComboTime = 0;
    private bool inCombo = false;

    public bool CanAttack { get; set; }

    // Components
    private PlayerPhysicsController characterController;
    private CharacterBase characterBase;
    private Camera cam;
    private AttackDirectionData nextAttackDirection;

    private void Awake()
    {
        inCombo = false;
        CanAttack = true;
        animator = GetComponent<Animator>();
        characterController = GetComponent<PlayerPhysicsController>();
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
        if (characterController == null) return;

        if (!CanAttack || !GameManager.Instance.PlayerControl) return;
        characterController.DoMovement = false;

        // Deal damage here
        DealDamage();
        AudioManager.Instance.Play("sfxPlayerAttack1");
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
        AttackDirectionData attackData = GetAttackData();

        StartCoroutine(DealDelayedDamagedRoutine(attackData, dashAttackDamageDelay));
    }

    private IEnumerator DealDelayedDamagedRoutine(AttackDirectionData attackData, float applyDamageDelay)
    {
        bool finished = false;
        ParticleSystem attackParticle = attackData.attackParticle;
        attackParticle.Play();

        while (!finished)
        {
            yield return new WaitForSeconds(applyDamageDelay);

            //Collider2D[] detectedDamageables = Physics2D.OverlapCircleAll(damageColliderPos.position, attackRange, enemyFilter);
            Collider2D[] detectedDamageables = Physics2D.OverlapBoxAll(attackData.attackLocation.position, attackData.attackDirectionColliderSize, enemyFilter);

            if (detectedDamageables.Length > 0)
            {
                for (int i = 0; i < detectedDamageables.Length; i++)
                {
                    if (detectedDamageables[i].gameObject != gameObject)
                    {
                        IDamageable damageable = detectedDamageables[i].GetComponent<IDamageable>();

                        if (damageable != null)
                        {
                            damageable.TakeDamage(this.gameObject.transform, damageAmount);
                        }
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

    private AttackDirectionData GetAttackData()
    {
        float attackDirection = characterBase.FacingDirection;
        AttackDirectionData attackData = attackDirectionData[(int)attackDirection];
        nextAttackDirection = attackData;

        return attackData;
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying && debug)
        {
            // Attack location

            Gizmos.color = Color.red;

            //Gizmos.DrawSphere(circleLocation.position, attackRange);
            Gizmos.DrawCube(nextAttackDirection.attackLocation.position, new Vector2(1.166171f, 1.638526f));

            // Attack Range
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(nextAttackDirection.attackLocation.position, new Vector2(1.166171f, 1.638526f));
        }
    }
}


