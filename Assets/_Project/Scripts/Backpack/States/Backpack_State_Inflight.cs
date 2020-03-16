using System;
using UnityEngine;
using DG.Tweening;

public class Backpack_State_Inflight : IState
{
    private Backpack backpack;
    private BackpackFX backpackFX;

    public Backpack_State_Inflight(Backpack _backpack)
    {
        backpack = _backpack;
        backpackFX = backpack.BackpackFX;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.INFLIGHT;

        backpackFX.ToggleTrails(true);
        backpackFX.SwitchHasBackback(false);
        backpackFX.ShowBackpack();

        backpack.RBody.DOMove(backpack.TeleportDestination, backpack.MovementTime, false).OnComplete(OnMoveCompleted);
    }

    public void Update()
    {
        if (InputManager.Instance.InputActions.Backpack.Teleport.triggered)
        {
            backpack.RBody.DOKill();

            backpack.Player.Teleport(backpack.transform.position);
            backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
        }
    }

    public void FixedUpdate() { }
    public void Exit() { }

    private void OnMoveCompleted()
    {
        backpack.stateMachine.ChangeState(new Backpack_State_Idle(backpack));
    }
}