using System;
using UnityEngine;

public class Backpack_State_Returning : IState
{
    private Backpack backpack;

    public Backpack_State_Returning(Backpack _backpack)
    {
        backpack = _backpack;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.RETURNING;
        backpack.BackpackMovement.MoveToPoint(backpack.Player.RBody2D.position, true, 0f, OnMoveCompleted);
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }

    private void OnMoveCompleted()
    {
        backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
    }
}