using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity_State_Attack : IState
{
    private BaseEnemy entity;
    private Transform target;

    private bool isAttackingRanged = false;
    private int remainingRangedAttacks = 3;
    private float currentMeleeAttackTime = 0f;
    private float currentRangeAttackTime = 0f;

    public BaseEntity_State_Attack(BaseStateMachineEntity _entity)
    {
        entity = _entity as BaseEnemy;
    }

    public void Initialize()
    {
        target = GameManager.Instance.Player.transform;
        Debug.Log("Entered attack state");
    }

    public void Update()
    {
        float distance = Vector2.Distance(entity.transform.position, target.position);

        if(isAttackingRanged)
        {
            RangedAttack();
            return;
        }

        if(distance > entity.MaxDetectionRange)
        {
            entity.stateMachine.ChangeState(new BaseEntity_State_Chase(entity));
            return;
        }

        if(distance > entity.TriggerRangedAttackMaxDistance)
        {
            isAttackingRanged = true;
            remainingRangedAttacks = entity.RangedAttackAmount;
            return;
        }

        MeleeAttack();

    }

    public void FixedUpdate()
    {
        
    }
    public void Exit()
    {

    }

    private void MeleeAttack()
    {
        if(currentMeleeAttackTime > 0)
        {
            currentMeleeAttackTime -= Time.deltaTime;
        }
        else
        {
            currentMeleeAttackTime = entity.MeleeAttackCooldown;
            entity.AttackManager.MeleeAttack(entity.MeleeAttackDamage);
        }
    }

    private void RangedAttack()
    {
        if(remainingRangedAttacks > 0)
        {
            if(currentRangeAttackTime > 0)
            {
                currentRangeAttackTime -= Time.deltaTime;
                return;
            }

            remainingRangedAttacks -= 1;
            currentRangeAttackTime = entity.RangedAttackCooldown;

            Vector2 direction = entity.transform.position.DirectionTo(target.position);
            entity.AttackManager.RangedAttack(direction.normalized, entity.RangedAttackDamage);
        }
        else
        {
            isAttackingRanged = false;
            entity.stateMachine.ChangeState(new BaseEntity_State_Chase(entity));
        }
    }
}
