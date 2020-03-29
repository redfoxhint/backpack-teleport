using UnityEngine;

public class BaseEntity_State_TakingDamage : IState
{
    private BaseEntity entity;

    public BaseEntity_State_TakingDamage(BaseEntity _entity)
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
        entity.stateMachine.ChangeState(new BaseEntity_State_Patrol(entity));
        Debug.Log("No longer taking Damage");
    }
}
