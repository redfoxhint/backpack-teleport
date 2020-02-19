using System;
using UnityEngine;

public class Backpack_State_Inflight : IState
{
    private Backpack backpack;
    private BackpackMovement backpackMovement;
    private BackpackFX backpackFX;

    public Backpack_State_Inflight(Backpack _backpack)
    {
        backpack = _backpack;
        backpackMovement = backpack.BackpackMovement;
        backpackFX = backpack.BackpackFX;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.INFLIGHT;

        backpackFX.ToggleTrails(true);
        backpackFX.SwitchHasBackback(false);
        backpackFX.ShowBackpack();

        backpack.BackpackMovement.MoveToPoint(backpack.TeleportDestination, true, 0f, OnMoveCompleted);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            backpack.Player.Teleport(backpack.transform.position);
            backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
        }
    }

    public void Exit()
    {
        backpackMovement.CancelMovement();
    }

    private void OnMoveCompleted()
    {
        backpack.stateMachine.ChangeState(new Backpack_State_Idle(backpack));
    }
}