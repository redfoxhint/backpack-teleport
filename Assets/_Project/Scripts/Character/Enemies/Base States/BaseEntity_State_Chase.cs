using System;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;
using UnityEngine.InputSystem;

public class BaseEntity_State_Chase : IState
{
    private BaseStateMachineEntity entity;
    private Transform target;

    public BaseEntity_State_Chase(BaseStateMachineEntity _entity)
    {
        entity = _entity;
        entity.Agent.OnDestinationReached += OnEntityReachedDestination;
    }

    public void Initialize()
    {
        target = GameManager.Instance.Player.transform;
        Debug.Log("Spider entered idle state");
    }

    public void Update()
    {
        Debug.Log("Chasing");
        entity.Agent.SetDestination(target.position);

        float distance = Vector2.Distance(entity.transform.position, target.position);

        if (distance > entity.MaxDetectionRange)
        {
            entity.stateMachine.ChangeState(new BaseEntity_State_Idle(entity));
        }
    }

    public void FixedUpdate()
    {
        entity.PhysicsController.SetMoveDirection(entity.Agent.Velocity.normalized);
    }

    public void Exit()
    {
        entity.Agent.Stop();
        entity.PhysicsController.SetMoveDirection(Vector2.zero);
    }

    private void OnEntityReachedDestination()
    {
        entity.stateMachine.ChangeState(new BaseEntity_State_Attack(entity));
    }
}