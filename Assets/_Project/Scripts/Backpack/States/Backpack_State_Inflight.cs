using System;
using UnityEngine;

public class Backpack_State_Inflight : IState
{
    private StateMachineUnit owner;

    private Backpack backpack;
    private BackpackFX backpackFX;

    public Backpack_State_Inflight(StateMachineUnit _owner)
    {
        owner = _owner;
        backpack = _owner.GetComponent<Backpack>();
        backpackFX = backpack.BackpackFX;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.INFLIGHT;
        backpackFX.ToggleTrails(true);
        backpackFX.SwitchHasBackback(false);
        backpackFX.ShowBackpack();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (backpack.LaunchTask != null) backpack.LaunchTask.Stop();
            backpack.Owner.Teleport(backpack.transform.position);
            backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
        }
    }

    public void Exit()
    {
        Debug.Log("Backpack has exited inflight");
    }
}