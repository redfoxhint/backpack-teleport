using UnityEngine;
public class VoidState : IState
{
    private StateMachineUnit owner;
    
    public VoidState(StateMachineUnit _owner)
    {
        owner = _owner;
    }

    public void Initialize()
    {
        Debug.Log($"{owner.gameObject.name}: Default state not found. Void state initialized.");
    }
    public void Update()
    {
        
    }
    public void Exit()
    {
        
    }
}
