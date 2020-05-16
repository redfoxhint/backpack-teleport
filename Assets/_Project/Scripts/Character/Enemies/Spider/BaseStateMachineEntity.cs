using BackpackTeleport.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;
using Pathfinding;

[RequireComponent(typeof(BaseDamageable), typeof(Rigidbody2D), typeof(CharacterAnimator))]
public abstract class BaseStateMachineEntity : StateMachineUnit
{
    // Public Variables
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected BaseCharacterData baseCharacterData;

    [Header("Stun Configuration")]
    [SerializeField] private float test;

    // Private Variables

    // Components
    public BaseDamageable BaseDamageable { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public CharacterAnimator CharacterAnimator { get; private set; }
    public AIPath Pathfinding {get; private set;}

    private void Awake()
    {
        if (!BaseDamageable) BaseDamageable = GetComponent<BaseDamageable>();
        if (!Rigidbody) Rigidbody = GetComponent<Rigidbody2D>();
        if (!CharacterAnimator) CharacterAnimator = GetComponent<CharacterAnimator>();
        if (!Pathfinding) Pathfinding = GetComponent<AIPath>();

        if (Pathfinding != null)
        {
            ConfigurePathfinding();
        }
            

        if (BaseDamageable != null)
        {
            BaseDamageable.onTookDamage += OnTookDamage;
            BaseDamageable.onDeath += OnDeath;
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void ConfigurePathfinding()
    {
        Pathfinding.maxSpeed = speed;
    }

    protected abstract void OnTookDamage();
    protected abstract void OnDeath();

}



