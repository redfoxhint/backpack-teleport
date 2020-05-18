using BackpackTeleport.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;
using System;

[RequireComponent(typeof(BaseDamageable), typeof(Rigidbody2D), typeof(CharacterAnimator))]
public abstract class BaseStateMachineEntity : StateMachineUnit, IWalkable
{
    // Public Variables
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected BaseCharacterData baseCharacterData;
    [SerializeField] private float maxDetectionRange = 15f;

    //[Header("Stun Configuration")]
    //[SerializeField] private float test;

    // Private Variables

    // Components
    public BaseDamageable BaseDamageable { get; private set; }
    public EnemyAttackManager AttackManager { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public CharacterAnimator CharacterAnimator { get; private set; }
    public PolyNavAgent Agent { get; private set; }
    public EntityPhysicsController PhysicsController { get; private set; }
    public float MaxDetectionRange { get => maxDetectionRange; }


    private void Awake()
    {
        if (!BaseDamageable) BaseDamageable = GetComponent<BaseDamageable>();
        if (!AttackManager) AttackManager = GetComponent<EnemyAttackManager>();
        if (!Rigidbody) Rigidbody = GetComponent<Rigidbody2D>();
        if (!CharacterAnimator) CharacterAnimator = GetComponent<CharacterAnimator>();
        if (!Agent) Agent = GetComponent<PolyNavAgent>();
        if (!PhysicsController) PhysicsController = GetComponent<EntityPhysicsController>();

        if (BaseDamageable != null)
        {
            BaseDamageable.onTookDamage += OnTookDamage;
            BaseDamageable.onStunFinished += OnStunFinished;
            BaseDamageable.onDeath += OnDeath;
        }
    }

    private void OnStunFinished()
    {
        PhysicsController.enabled = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void ToggleMovement(bool toggle)
    {
        //if (Agent == null) return;

        //Agent.enabled = toggle;
    }

    public float GetDistanceFromTarget(Transform target)
    {
        return Vector2.Distance(transform.position, target.position);
    }

    protected virtual void OnTookDamage()
    {
        PhysicsController.enabled = false;
    }

    protected abstract void OnDeath();
    public abstract void SetWalkableVelocity(Vector3 velocity);
}



