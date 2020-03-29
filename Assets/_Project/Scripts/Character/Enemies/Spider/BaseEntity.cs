using BackpackTeleport.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseDamageable), typeof(Rigidbody2D), typeof(PolyNav.PolyNavAgent))]
public class BaseEntity : StateMachineUnit
{
    // Public Variables
    [SerializeField] private List<Vector2> path = new List<Vector2>();

    // Private Variables
    private PolyNav.PolyNav2D polyNav;

    // Components
    public BaseDamageable baseDamageable { get; private set; }
    public PolyNav.PolyNavAgent agent { get; private set; }
    public EntityPhysicsController controller { get; private set; }

    private void Awake()
    {
        baseDamageable = GetComponent<BaseDamageable>();
        agent = GetComponent<PolyNav.PolyNavAgent>();
        controller = GetComponent<EntityPhysicsController>();

        polyNav = agent.map;
        agent.Stop();


        baseDamageable.onTookDamage += OnTookDamage;

    }

    protected override void Start()
    {
        stateMachine.ChangeState(new BaseEntity_State_Patrol(this));
    }

    protected override void Update()
    {
        base.Update();
        path = agent.activePath;
    }

    public void OnTookDamage()
    {
        stateMachine.ChangeState(new BaseEntity_State_TakingDamage(this));
    }
}



