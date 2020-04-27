using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Backpack_State_Aiming : IState
{
    private Backpack backpack;
    private InputActions inputActions;
    private bool isReturning = false;

    public Backpack_State_Aiming(Backpack _backpack)
    {
        backpack = _backpack;
        inputActions = InputManager.Instance.InputActions;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.AIMING;
        backpack.IsAiming = true;

        InputManager.Instance.InputActions.Backpack.Aim.canceled += OnAimKeyReleased;
        backpack.AimingAnimation.RadiusCircleController.Activate();

        backpack.RBody.MovePosition(GameManager.Instance.Player.RBody2D.position);
    }

    public void Update()
    {
        if (GameManager.Instance.Player == null) return;

        backpack.PointA = GameManager.Instance.Player.PlayerCenter.position;
        backpack.PointB = backpack.GameCursor.transform.position;

        DrawAimingUI();

        if(!backpack.IsAiming)
        {
            backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
        }

        if(InputManager.Instance.InputActions.Backpack.ThrowBackpack.triggered)
        {
            backpack.Launch(backpack.PointB);

            if (GameEvents.onBackpackThrownEvent != null)
            {
                GameEvents.onBackpackThrownEvent.Invoke();
            }

            backpack.stateMachine.ChangeState(new Backpack_State_Inflight(backpack));
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
        backpack.IsAiming = false;
        InputManager.Instance.InputActions.Backpack.Aim.canceled -= OnAimKeyReleased;
        backpack.AimingAnimation.RadiusCircleController.Deactivate();
        backpack.AimingAnimation.DisableArrow();
    }

    private void OnAimKeyReleased(InputAction.CallbackContext value)
    {
        backpack.IsAiming = false;
    }

    private void DrawAimingUI()
    {
        backpack.AimingAnimation.DrawDottedLineAndArrow(backpack.PointA, backpack.PointB);
    }
}