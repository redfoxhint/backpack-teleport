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
    }

    public void Initialize()
    {
        Debug.Log("Entered Chase State");
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
        //entity.controller.SetMoveDirection(Vector2.zero);
        //Debug.Log("Exited Patrol State");
    }
}