using System;
using UnityEngine;
using DG.Tweening;

public class Backpack_State_Returning : IState
{
    private Backpack backpack;

    public Backpack_State_Returning(Backpack _backpack)
    {
        backpack = _backpack;
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.RETURNING;
        backpack.RBody.DOMove(GameManager.Instance.Player.transform.position, backpack.MovementTime / 2f, false).OnComplete(OnMoveCompleted);
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {

    }

    private void OnMoveCompleted()
    {
        backpack.stateMachine.ChangeState(new Backpack_State_Inhand(backpack));
    }
}