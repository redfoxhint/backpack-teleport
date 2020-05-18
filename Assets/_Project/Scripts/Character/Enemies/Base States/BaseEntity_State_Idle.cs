using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity_State_Idle : IState
{
    private BaseStateMachineEntity entity;
    private Transform target;

    public BaseEntity_State_Idle(BaseStateMachineEntity _entity)
    {
        entity = _entity;
    }

    public void Initialize()
    {
        target = GameManager.Instance.Player.transform;
        Debug.Log("Spider entered idle state");
    }

    public void Update()
    {
        float distance = Vector2.Distance(entity.transform.position, target.position);

        if(distance < entity.MaxDetectionRange)
        {
            entity.stateMachine.ChangeState(new BaseEntity_State_Chase(entity));
        }
    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {
        
    }
}
