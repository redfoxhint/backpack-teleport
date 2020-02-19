using System;
using UnityEngine;

public class Backpack_State_Inhand : IState
{
    private Backpack backpack;
    private BackpackFX backpackFX;
    private Rigidbody2D rBody;

    public Backpack_State_Inhand(Backpack _backpack)
    {
        backpack = _backpack;
        backpackFX = backpack.BackpackFX;
        rBody = backpack.RBody;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.INHAND;
        backpackFX.ToggleTrails(false);
        backpackFX.SwitchHasBackback(true);
        backpackFX.RippleEffect(backpack.transform.position);
        backpackFX.HideBackpack();

        backpack.IsAiming = false;
    }

    public void Update()
    {
        rBody.position = backpack.Player.RBody2D.position;

        if (Input.GetKeyDown(backpack.AimKey))
        {
            backpack.stateMachine.ChangeState(new Backpack_State_Aiming(backpack));
        }
    }

    public void Exit()
    {

    }

}