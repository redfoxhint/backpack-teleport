using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackTeleport.Character.PlayerCharacter;

public enum BackpackStates
{
    AIMING, INFLIGHT, IDLE, RETURNING, INHAND
}

[RequireComponent(typeof(BackpackFX), typeof(BackpackMovement), typeof(Rigidbody2D))]
public class Backpack : StateMachineUnit, IActivator
{
    // Public Variables
    [SerializeField] private KeyCode aimKey;
    public KeyCode AimKey { get => aimKey; }

    [SerializeField] private float maxThrowDistance = 300f;
    public float MaxThrowDistance = 300f;

    [SerializeField] private bool canBeAimed;
    public bool CanBeAimed { get => canBeAimed; set => canBeAimed = value; }

    [SerializeField] private bool chainReady;
    public bool ChainReady { get => chainReady; set => chainReady = value; }

    [SerializeField] private bool isAiming;
    public bool IsAiming { get => isAiming; set => isAiming = value; }

    [SerializeField] private float movementSpeed;
    public float MovementSpeed { get => movementSpeed; }

    [SerializeField] private Vector2 teleportDestination;
    public Vector2 TeleportDestination { get => teleportDestination; }

    [SerializeField] private BackpackStates currentState;
    public BackpackStates CurrentState { get => currentState; set => currentState = value; }

    // Private Variables
    private float calculatedBackpackTravelDistance;
    public float CalculatedTravelDistance { get => calculatedBackpackTravelDistance; set => calculatedBackpackTravelDistance = value; }

    private Vector2 pointA;
    public Vector2 PointA { get => pointA; set => pointA = value; }

    private Vector2 pointB;
    public Vector2 PointB { get => pointB; set => pointB = value; }

    // Components
    private Player player;
    public Player Player { get => player; }

    private BackpackFX backpackFX;
    public BackpackFX BackpackFX { get => backpackFX; }

    private BackpackChaining backpackChaining;
    public BackpackChaining BackpackChaining { get => backpackChaining; }

    private BackpackMovement backpackMovement;
    public BackpackMovement BackpackMovement { get => backpackMovement; }

    private AimingAnimation aimingAnimation;
    public AimingAnimation AimingAnimation { get => aimingAnimation; }

    private Camera cam;
    public Camera Cam { get => cam; }

    private Rigidbody2D rBody;
    public Rigidbody2D RBody { get => rBody; }

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        backpackFX = GetComponent<BackpackFX>();
        backpackChaining = GetComponent<BackpackChaining>();
        backpackMovement = GetComponent<BackpackMovement>();
        aimingAnimation = player.GetComponent<AimingAnimation>();
        cam = Camera.main;
    }

    protected override void Start()
    {
        stateMachine.ChangeState(new Backpack_State_Inhand(this));
        canBeAimed = true;
    }

    protected override void Update()
    {
        base.Update();
    }

    public void Launch(Vector2 pos)
    {
        teleportDestination = pos;
        rBody.position = player.RBody2D.position; // Set the position of the backpack to the players location so it looks like it spawns from the players center.
    }

    public void UpdateTeleportationDestination(Vector2 pos)
    {
        teleportDestination = pos;
    }
}
