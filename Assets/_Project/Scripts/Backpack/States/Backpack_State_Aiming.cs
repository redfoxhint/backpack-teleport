﻿using System;
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
    }

    public void Update()
    {
        backpack.RBody.MovePosition(backpack.Player.RBody2D.position);

        backpack.PointA = backpack.Player.PlayerCenter.position;
        backpack.PointB = backpack.GameCursor.transform.position;

        backpack.CalculatedTravelDistance = (backpack.PointB - backpack.PointA).sqrMagnitude;

        //HandleBackpackAimingUI();
        backpack.AimingAnimation.DrawDottedLineAndArrow(backpack.PointA, backpack.PointB);

        if(inputActions.Backpack.Return.triggered)
        {
            backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
            return;
        }

        if (!backpack.IsAiming)
        {
            if(backpack.AimingAnimation.IsOverlappingObstacle(backpack.PointA, backpack.PointB))
            {
                backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
                return;
            }

            if(backpack.CalculatedTravelDistance <= backpack.MaxThrowDistance)
            {
                backpack.Launch(backpack.PointB);

                if (GameEvents.onBackpackThrownEvent != null)
                {
                    GameEvents.onBackpackThrownEvent.Invoke(backpack);
                }

                backpack.stateMachine.ChangeState(new Backpack_State_Inflight(backpack));
            }
            else
            {
                backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
            }
        }
    }

    public void Exit()
    {
        backpack.IsAiming = false;
        InputManager.Instance.InputActions.Backpack.Aim.canceled -= OnAimKeyReleased;
        backpack.AimingAnimation.RadiusCircleController.Deactivate();
    }

    private void HandleBackpackAimingUI()
    {
        if (backpack.CalculatedTravelDistance <= backpack.MaxThrowDistance)
            backpack.AimingAnimation.UpdateUI(backpack.CalculatedTravelDistance, Color.white);
        else
            backpack.AimingAnimation.UpdateUI(backpack.CalculatedTravelDistance, Color.red);

        if (backpack.AimingAnimation.AimAngle > 90f || backpack.AimingAnimation.AimAngle < -90f)
            backpack.AimingAnimation.RotateText(-180f);
        else
            backpack.AimingAnimation.RotateText(0f);
    }


    private void OnAimKeyReleased(InputAction.CallbackContext value)
    {
        backpack.IsAiming = false;
    }
}