using System;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;
using UnityEngine.InputSystem;

public class BaseEntity_State_Patrol : IState
{
    private BaseEntity entity;
    private PlayerDamageable player;

    public BaseEntity_State_Patrol(BaseEntity _entity)
    {
        entity = _entity;
        player = GameManager.Instance.Player.GetComponent<PlayerDamageable>();
    }

    public void Initialize()
    {
        Debug.Log("Entered Patrol State");
    }

    public void Update()
    {
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