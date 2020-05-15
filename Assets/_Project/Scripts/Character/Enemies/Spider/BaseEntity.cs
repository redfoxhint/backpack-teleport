using BackpackTeleport.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;

[RequireComponent(typeof(BaseDamageable), typeof(Rigidbody2D), typeof(PolyNav.PolyNavAgent))]
public class BaseEntity : StateMachineUnit
{
    // Public Variables

    // Private Variables

    // Components
    public BaseDamageable baseDamageable { get; private set; }
    public EntityPhysicsController controller { get; private set; }

    private void Awake()
    {
        baseDamageable = GetComponent<BaseDamageable>();
        controller = GetComponent<EntityPhysicsController>();

        baseDamageable.onTookDamage += OnTookDamage;
    }

    protected override void Start()
    {
        base.Start();
        //stateMachine.ChangeState(new BaseEntity_State_Patrol(this));
    }

    protected override void Update()
    {
        base.Update();
    }

    public void OnTookDamage()
    {
        //stateMachine.ChangeState(new BaseEntity_State_TakingDamage(this));
    }
}



