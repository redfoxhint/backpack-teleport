using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Backpack_State_Inhand : IState
{
    private Backpack backpack;
    private BackpackFX backpackFX;
    private Rigidbody2D rBody;

    private bool isAiming = false;

    public Backpack_State_Inhand(Backpack _backpack)
    {
        backpack = _backpack;
        backpackFX = backpack.BackpackFX;
        rBody = backpack.RBody;

        InputManager.Instance.InputActions.Backpack.Aim.performed += OnAimKeyPressed;
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

        if (isAiming)
        {
            backpack.stateMachine.ChangeState(new Backpack_State_Aiming(backpack));
        }
    }

    public void Exit()
    {
        InputManager.Instance.InputActions.Backpack.Aim.performed -= OnAimKeyPressed;
    }

    private void OnAimKeyPressed(InputAction.CallbackContext value)
    {
        isAiming = true;
    }

}