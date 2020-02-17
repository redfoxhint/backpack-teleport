using System;
using UnityEngine;

public class Backpack_State_Returning : IState
{
    private StateMachineUnit owner;
    private Backpack backpack;

    public Backpack_State_Returning(StateMachineUnit _owner)
    {
        owner = _owner;
        backpack = _owner.GetComponent<Backpack>();
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.RETURNING;
        backpack.ReturningTask = new Task(backpack.ReturnToPlayerWithCurve(backpack.MovementSpeed * 2, backpack.movementCurve));
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}