using BackpackTeleport.Character;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseStateMachineEntity, IWalkable
{
    [Header("Base Enemy Configuration")]

    //[SerializeField] private AIPath aiPath;
    [SerializeField] private CharacterAnimator characterBase;
    [SerializeField] private float attackRange = 10f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ToggleMovement(true);
    }

    // Update is called once per frame
    protected override void Update()
    {
        characterBase.SetAnimatorParameters(Pathfinding.velocity);

        if(CanChase())
        {
            ToggleMovement(true);
        }
        else
        {
            ToggleMovement(false);
        }
    }


    private bool CanChase()
    {
        return Vector2.Distance(transform.position, Pathfinding.destination) < attackRange && !BaseDamageable.IsStunned;
    }

    protected override void OnDeath()
    {
        
    }

    public void ToggleMovement(bool toggle)
    {
        if(Pathfinding != null)
        {
            Pathfinding.enabled = toggle;
        }
    }

    public void SetWalkableVelocity(Vector3 velocity) { }

    protected override void OnTookDamage() { }
}
