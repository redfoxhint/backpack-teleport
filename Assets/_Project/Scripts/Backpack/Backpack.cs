using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackTeleport.Character.PlayerCharacter;
using UnityEditor;

public enum BackpackStates
{
    AIMING, INFLIGHT, IDLE, RETURNING, INHAND, CHAINING, CHAINING_SETUP
}

[RequireComponent(typeof(BackpackFX), typeof(BackpackMovement), typeof(Rigidbody2D))]
public class Backpack : StateMachineUnit, IActivator
{
    // Public Variables
    [SerializeField] private KeyCode aimKey;
    public KeyCode AimKey { get => aimKey; }

    [SerializeField] private float maxThrowDistance = 300f;
    public float MaxThrowDistance = 300f;

    [SerializeField] private float movementSpeed;
    public float MovementSpeed { get => movementSpeed; }

    [Header("Chaining Configuration")]
    [SerializeField] private int maxMarkerPositions = 2;
    public int MaxMarkerPositions { get => maxMarkerPositions; }

    [SerializeField] private GameObject markerPrefab;
    public GameObject MarkerPrefab { get => markerPrefab; }

    [SerializeField] private bool chainReady;
    public bool ChainReady { get => chainReady; set => chainReady = value; }

    [SerializeField] private bool isAiming;
    public bool IsAiming { get => isAiming; set => isAiming = value; }

    [SerializeField] private Vector2 teleportDestination;
    public Vector2 TeleportDestination { get => teleportDestination; }

    [SerializeField] private BackpackStates currentState;
    public BackpackStates CurrentState 
    { 
        get => currentState; 
        set
        {
            currentState = value;
            //Debug.Log($"Backpack entered: <color=red>{value}</color>");
        }
    }

    // Private Variables
    private float calculatedBackpackTravelDistance;
    public float CalculatedTravelDistance { get => calculatedBackpackTravelDistance; set => calculatedBackpackTravelDistance = value; }

    private Vector2 pointA;
    public Vector2 PointA { get => pointA; set => pointA = value; }

    private Vector2 pointB;
    public Vector2 PointB { get => pointB; set => pointB = value; }

    // Chaining Variables
    private Vector2 nextTeleportLocation;
    public Vector2 NextTeleportLocation { get => nextTeleportLocation; set => nextTeleportLocation = value; }

    // Components
    private Player player;
    public Player Player { get => player; }

    private BackpackFX backpackFX;
    public BackpackFX BackpackFX { get => backpackFX; }

    private BackpackMovement backpackMovement;
    public BackpackMovement BackpackMovement { get => backpackMovement; }

    private AimingAnimation aimingAnimation;
    public AimingAnimation AimingAnimation { get => aimingAnimation; }

    private Camera cam;
    public Camera Cam { get => cam; }

    private Rigidbody2D rBody;
    public Rigidbody2D RBody { get => rBody; }

    private GameCursor gameCursor;
    public GameCursor GameCursor { get => gameCursor; }
    

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        gameCursor = FindObjectOfType<GameCursor>();
        backpackFX = GetComponent<BackpackFX>();
        backpackMovement = GetComponent<BackpackMovement>();
        aimingAnimation = player.GetComponent<AimingAnimation>();
        cam = Camera.main;
    }

    protected override void Start()
    {
        stateMachine.ChangeState(new Backpack_State_Inhand(this));
    }

    public void Launch(Vector2 pos)
    {
        teleportDestination = pos;
        rBody.position = player.RBody2D.position; // Set the position of the backpack to the players location so it looks like it spawns from the players center.
    }
}