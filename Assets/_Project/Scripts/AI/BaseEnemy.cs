using BackpackTeleport.Character;
using Pathfinding;
using PolyNav;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseEnemy : BaseStateMachineEntity
{
    [Header("Base Enemy Configuration")]

    //[SerializeField] private AIPath aiPath;
    [SerializeField] private CharacterAnimator characterBase;
    [SerializeField] private Vector2[] currentPath;

    [Header("Attack Configuration")]
    [SerializeField] private float meleeAttackDamage = 1f;
    [SerializeField] private float rangedAttackDamage = 1f;
    [SerializeField] private int rangedAttackAmount = 3;
    [SerializeField] private float triggerRangedAttackMaxDistance = 5f;
    [SerializeField] private float maxMeleeAttackRange = 2f;
    [SerializeField] private float meleeAttackWaitTime = 1f;
    [SerializeField] private float meleeAttackCooldown = 2f;
    [SerializeField] private float rangedAttackCooldown = 1f;
    

    public float MeleeAttackDamage { get => meleeAttackDamage; }
    public float RangedAttackDamage { get => rangedAttackDamage; }
    public int RangedAttackAmount { get => rangedAttackAmount; }
    public float MaxMeleeAttackRange { get => maxMeleeAttackRange; }
    public float TriggerRangedAttackMaxDistance { get => triggerRangedAttackMaxDistance; }
    public float MeleeAttackWaitTime { get => meleeAttackWaitTime; }
    public float MeleeAttackCooldown { get => meleeAttackCooldown; }
    public float RangedAttackCooldown { get => rangedAttackCooldown; }

    protected override void Start()
    {
        stateMachine.ChangeState(new BaseEntity_State_Idle(this));
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDeath()
    {
        
    }

    protected override void OnTookDamage() 
    {
        base.OnTookDamage();

        if (!BaseDamageable.Incapacitated)
            stateMachine.ChangeState(new BaseEntity_State_Stun(this));

    }

    public override void SetWalkableVelocity(Vector3 velocity)
    {
        
    }
}
