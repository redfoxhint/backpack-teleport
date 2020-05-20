using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity_State_Stun : IState
{
    private BaseStateMachineEntity entity;
    private Transform target;

    public BaseEntity_State_Stun(BaseStateMachineEntity _entity)
    {
        entity = _entity;
    }

    public void Initialize()
    {
        Debug.Log("Entered stun state");
        entity.BaseDamageable.onStunFinished += OnStunFinished;
    }

    public void Update()
    {
        Debug.Log("Stunned!");
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
        
    }

    private void OnStunFinished()
    {
        entity.stateMachine.ChangeState(new BaseEntity_State_Idle(entity));
    }
}
