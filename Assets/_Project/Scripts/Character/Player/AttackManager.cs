using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private List<AttackDirection> attackDirections;

    // Private Variables
    private float attackTime = 0.25f;
    private float attackCounter = 0.25f;
    private bool isAttacking;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        foreach(AttackDirection attackDirection in attackDirections)
        {
            attackDirection.onColliderHit = i => DealDamage(i, 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Attack()
    {
        if (isAttacking) return;

        anim.SetBool("isAttacking", true);
        attackCounter = attackTime;
        isAttacking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            attackCounter -= Time.deltaTime;
            if(attackCounter <= 0)
            {
                anim.SetBool("isAttacking", false);
                isAttacking = false;
            }
        }
    }

    // Called with animation event.
    public void ResetAttack()
    {
        //anim.SetBool("isAttacking", false);
    }

    private void DealDamage(IDamageable damageable, float amount)
    {
        damageable.TakeDamage(amount, Vector2.zero);
        Debug.Log($"{amount} was dealt.");
    }
}
