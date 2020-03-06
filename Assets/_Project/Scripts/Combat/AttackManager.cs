using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private float nextAttackTime = 1; // The time between attacks before the combo is reset.
    [SerializeField] private float dashAmount;
    [SerializeField] private bool doDashAttack = true;
    private Animator animator;

    private float comboIndex = 0;
    private float currentComboTime = 0;
    private bool inCombo = false;

    public bool CanAttack { get; set; }

    private PhysicsCharacterController characterController;

    private void Awake()
    {
        inCombo = false;
        CanAttack = true;
        animator = GetComponent<Animator>();
        characterController = GetComponent<PhysicsCharacterController>();

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

        if (!inCombo)
        {
            StartCombo();
            return;
        }

        comboIndex += 1;

        if(comboIndex == 3)
        {
            SetAttack(comboIndex);
            ResetCombo();
            return;
        }

        SetAttack(comboIndex);

        currentComboTime = nextAttackTime;
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

        if(doDashAttack)
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
}
