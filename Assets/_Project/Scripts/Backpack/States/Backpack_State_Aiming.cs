using System;
using UnityEngine;

public class Backpack_State_Aiming : IState
{
    private Backpack backpack;

    public Backpack_State_Aiming(Backpack _backpack)
    {
        backpack = _backpack;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.AIMING;
        backpack.IsAiming = true;
        Debug.Log("Backpack has entered the aiming state");
    }

    public void Update()
    {
        backpack.RBody.MovePosition(backpack.Player.RBody2D.position);

        backpack.PointA = backpack.Player.PlayerCenter.position;
        backpack.PointB = backpack.Cam.ScreenToWorldPoint(Input.mousePosition);

        backpack.CalculatedTravelDistance = (backpack.PointB - backpack.PointA).sqrMagnitude;

        HandleBackpackAimingUI();
        backpack.AimingAnimation.DrawDottedLineAndArrow(backpack.PointA, backpack.PointB);

        if(Input.GetKeyDown(KeyCode.R))
        {
            backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
        }


        if(Input.GetKeyUp(backpack.AimKey) && backpack.IsAiming)
        {
            if(backpack.CalculatedTravelDistance <= backpack.MaxThrowDistance)
            {
                backpack.Launch(backpack.PointB);
                backpack.stateMachine.ChangeState(new Backpack_State_Inflight(backpack));
                //backpack.Player.ThrowBackpack();
                if(GameEvents.onBackpackThrownEvent != null)
                {
                    GameEvents.onBackpackThrownEvent.Invoke(backpack);
                }
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
}