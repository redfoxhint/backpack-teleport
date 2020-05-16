using UnityEngine;

public class BaseEntity_State_TakingDamage : IState
{
    private BaseStateMachineEntity entity;

    public BaseEntity_State_TakingDamage(BaseStateMachineEntity _entity)
    {
        entity = _entity;
    }

    public void Initialize()
    {
        Debug.Log("Taking Damage");
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {
        entity.stateMachine.ChangeState(new BaseEntity_State_Chase(entity));
        Debug.Log("No longer taking Damage");
    }
}
