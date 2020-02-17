using System;
using UnityEngine;

public class Backpack_State_Idle : IState
{
    private StateMachineUnit owner;
    private Backpack backpack;
    private BackpackFX backpackFX;
    private BackpackChaining chaining;

    public Backpack_State_Idle(StateMachineUnit _owner)
    {
        owner = _owner;
        backpack = _owner.GetComponent<Backpack>();
        backpackFX = backpack.GetComponent<BackpackFX>();
        chaining = backpack.Chaining;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.IDLE;
        backpackFX.ToggleTrails(false);
    }

    public void Update()
    {
        if(chaining.ChainComplete)
        {
            if(!backpack.ChainReady)
            {
                chaining.InitializeChain();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            backpack.Owner.Teleport(backpack.TeleportDestination);
            backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
        }

        else if (Input.GetKey(KeyCode.LeftShift))
        {
            chaining.PlaceMarkerAtPosition(backpack.Cam.ScreenToWorldPoint(Input.mousePosition));
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            backpack.stateMachine.ChangeState(new Backpack_State_Returning(backpack));
            return;
        }
    }

    public void Exit()
    {

    }
}