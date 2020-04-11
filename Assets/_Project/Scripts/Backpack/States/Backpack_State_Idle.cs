using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Backpack_State_Idle : IState
{
    private Backpack backpack;
    private BackpackFX backpackFX;

    private bool chainStarted = false;

    public Backpack_State_Idle(Backpack _backpack)
    {
        backpack = _backpack;
        backpackFX = backpack.BackpackFX;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.IDLE;
        backpackFX.ToggleTrails(false);

        InputManager.Instance.InputActions.Backpack.Aim.started += StartChain;
    }

    public void Update()
    {
        if (InputManager.Instance.InputActions.Backpack.Teleport.triggered)
        {
            GameManager.Instance.Player.Teleport(backpack.transform.position);
            backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
        }

        //else if (chainStarted)
        //{
        //    backpack.stateMachine.ChangeState(new Backpack_State_Chaining_Setup(backpack));
        //    //chaining.PlaceMarkerAtPosition(backpack.Cam.ScreenToWorldPoint(Input.mousePosition));
        //}

        else if (InputManager.Instance.InputActions.Backpack.Return.triggered)
        {
            backpack.stateMachine.ChangeState(new Backpack_State_Returning(backpack));
            return;
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
        InputManager.Instance.InputActions.Backpack.Aim.started -= StartChain;
    }

    private void StartChain(InputAction.CallbackContext value)
    {
        backpack.stateMachine.ChangeState(new Backpack_State_Chaining_Setup(backpack));
    }
}