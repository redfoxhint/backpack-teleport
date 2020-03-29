using System;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;
using UnityEngine.InputSystem;

public class BaseEntity_State_Patrol : IState
{
    private BaseEntity entity;
    private PlayerDamageable player;
    private PolyNav2D polyNav;

    public BaseEntity_State_Patrol(BaseEntity _entity)
    {
        entity = _entity;
        player = GameObject.FindObjectOfType<PlayerDamageable>();
        polyNav = this.entity.agent.map;
    }

    public void Initialize()
    {
        Debug.Log("Entered Patrol State");
    }

    public void Update()
    {
        entity.agent.SetDestination(player.transform.position);
        Vector2 dirToDest = entity.transform.position.DirectionTo(entity.agent.nextPoint);
        entity.controller.SetMoveDirection(dirToDest);
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
        entity.controller.SetMoveDirection(Vector2.zero);
        Debug.Log("Exited Patrol State");
    }
}