using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineUnit : MonoBehaviour
{
    public readonly StateMachine stateMachine = new StateMachine();

    protected virtual void Start()
    {
        stateMachine.ChangeState(new VoidState(this));
    }

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
