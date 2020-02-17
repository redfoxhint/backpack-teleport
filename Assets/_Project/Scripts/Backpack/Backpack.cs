using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackTeleport.Character.PlayerCharacter;

public enum BackpackStates
{
    AIMING, INFLIGHT, IDLE, RETURNING, INHAND
}

[RequireComponent(typeof(BackpackFX), typeof(Rigidbody2D))]
public class Backpack : StateMachineUnit, IActivator
{
    // Public Variables
    [SerializeField] private float backpackMovementSpeed;
    public AnimationCurve movementCurve;

    // Properties
    public bool CanBeAimed { get; set; }
    public bool ChainReady { get; set; }
    public float MovementSpeed { get; private set; }
    public Vector2 TeleportDestination { get; private set; }
    public Player Owner { get; private set; }
    public BackpackFX BackpackFX { get; private set; }
    public BackpackChaining Chaining { get; private set; }
    public Task LaunchTask { get; private set; }
    public Task ReturningTask { get; set; }
    public Camera Cam { get; private set; }
    public BackpackStates CurrentState { get; set; }
  
    private Rigidbody2D rBody;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        Owner = FindObjectOfType<Player>();
        BackpackFX = GetComponent<BackpackFX>();
        Chaining = GetComponent<BackpackChaining>();
        Cam = Camera.main;
    }

    protected override void Start()
    {
        stateMachine.ChangeState(new Backpack_State_Inhand(this));
        CanBeAimed = true;
    }

    protected override void Update()
    {
        base.Update();
    }

    public void Launch(Vector2 pos)
    {
        if (LaunchTask != null)
        {
            LaunchTask.Stop();
        }

        LaunchTask = new Task(MoveToPointWithCurve(backpackMovementSpeed, pos, movementCurve));
        LaunchTask.Start();

        TeleportDestination = pos;
        rBody.position = Owner.RBody2D.position; // Set the position of the backpack to the players location so it looks like it spawns from the players center.
    }

    private IEnumerator MoveToPointWithCurve(float speed, Vector3 target, AnimationCurve animCurve)
    {
        Vector2 startPoint = rBody.position;
        float animationTimePosition = 0;
        target.z = 0;

        while (transform.position != target)
        {
            animationTimePosition += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(startPoint, target, animCurve.Evaluate(animationTimePosition));
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            yield return null;
        }

        stateMachine.ChangeState(new Backpack_State_Idle(this));

        yield break;
    }

    public IEnumerator ReturnToPlayerWithCurve(float speed, AnimationCurve animCurve)
    {
        Vector2 startPoint = rBody.position;
        float animationTimePosition = 0;

        while (Vector3.Distance(Owner.RBody2D.position, rBody.position) > 2f)
        {
            animationTimePosition += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(startPoint, Owner.RBody2D.position, animCurve.Evaluate(animationTimePosition));
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            yield return null;
        }

        stateMachine.ChangeState(new Backpack_State_Inhand(this));

        yield break;
    }
}
