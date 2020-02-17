using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineUnit : MonoBehaviour
{
    private StateMachine stateMachine = new StateMachine();

    private void Start()
    {
        stateMachine.ChangeState(new VoidState(this));
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
