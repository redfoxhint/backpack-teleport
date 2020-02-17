using System;
using UnityEngine;

public class Backpack_State_Inhand : IState
{
    private StateMachineUnit owner;

    private Backpack backpack;
    private BackpackFX backpackFX;
    private Rigidbody2D rBody;

    public Backpack_State_Inhand(StateMachineUnit _owner)
    {
        owner = _owner;
        backpack = _owner.GetComponent<Backpack>();
        backpackFX = backpack.GetComponent<BackpackFX>();
        rBody = backpack.GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.INHAND;
        backpackFX.ToggleTrails(false);
        backpackFX.SwitchHasBackback(true);
        backpackFX.RippleEffect(backpack.transform.position);
        backpackFX.HideBackpack();
    }

    public void Update()
    {
        rBody.position = backpack.Owner.RBody2D.position;
    }

    public void Exit()
    {

    }
}