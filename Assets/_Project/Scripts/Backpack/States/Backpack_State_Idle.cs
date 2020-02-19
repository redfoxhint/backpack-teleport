using System;
using UnityEngine;

public class Backpack_State_Idle : IState
{
    private Backpack backpack;
    private BackpackFX backpackFX;

    public Backpack_State_Idle(Backpack _backpack)
    {
        backpack = _backpack;
        backpackFX = backpack.BackpackFX;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.IDLE;
        backpackFX.ToggleTrails(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            backpack.Player.Teleport(backpack.transform.position);
            backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
        }

        else if (Input.GetKey(KeyCode.LeftShift))
        {
            backpack.stateMachine.ChangeState(new Backpack_State_Chaining_Setup(backpack));
            //chaining.PlaceMarkerAtPosition(backpack.Cam.ScreenToWorldPoint(Input.mousePosition));
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