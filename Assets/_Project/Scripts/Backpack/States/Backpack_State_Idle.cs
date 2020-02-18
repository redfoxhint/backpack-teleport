using System;
using UnityEngine;

public class Backpack_State_Idle : IState
{
    private Backpack backpack;
    private BackpackFX backpackFX;
    private BackpackChaining chaining;

    public Backpack_State_Idle(Backpack _backpack)
    {
        backpack = _backpack;
        backpackFX = backpack.BackpackFX;
        chaining = backpack.BackpackChaining;
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
            backpack.Player.Teleport(backpack.TeleportDestination);
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