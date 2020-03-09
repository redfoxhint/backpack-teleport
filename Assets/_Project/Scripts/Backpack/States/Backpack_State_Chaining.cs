using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Collections;

[Serializable]
public class Backpack_State_Chaining : IState
{
    private Backpack backpack;
    private BackpackChainData chainData;

    private int teleportsLeft;

    public Backpack_State_Chaining(Backpack _backpack, BackpackChainData _chainData)
    {
        backpack = _backpack;
        chainData = _chainData;
        teleportsLeft = chainData.markerPositions.Count;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.CHAINING;
    }

    public void Update()
    {
        if(teleportsLeft > 0)
        {
            if(InputManager.Instance.InputActions.Backpack.Teleport.triggered)
            {
                TeleportToNextMarker();
                backpack.BackpackFX.RippleEffect(backpack.transform.position);
                teleportsLeft -= 1;
            }
        }
        else
        {
            backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
        
    }


    private void TeleportToNextMarker()
    {
        GameObject nextMarker = GetNextMarker();
        backpack.Player.Teleport(nextMarker.transform.position);

        chainData.DeleteMarker(nextMarker);

        chainData.markerPositions.Dequeue();
    }

    private GameObject GetNextMarker()
    {
        return chainData.markerPositions.Peek();
    }
}
