using System;
using UnityEngine;

public class Backpack_State_Aiming : IState
{
    private StateMachineUnit owner;
    private Backpack backpack;

    public Backpack_State_Aiming(StateMachineUnit _owner)
    {
        owner = _owner;
        backpack = _owner.GetComponent<Backpack>();
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.AIMING;
        Debug.Log("Backpack has entered the aiming state");
    }

    public void Update()
    {

    }

    public void Exit()
    {
        
    }
}