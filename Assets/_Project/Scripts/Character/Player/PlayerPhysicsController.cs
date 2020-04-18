using UnityEngine;
using System.Collections;

public class PlayerPhysicsController : PhysicsCharacterController
{
    [Header("Character Controller Configuration")]
    [SerializeField] private LayerMask dashFilter;
    [SerializeField] private ParticleSystem dustParticles;

    [Header("Dash Configuration")]
    [SerializeField] private float dashAmount;

    private bool isDashButtonDown = false;
    private DashAbility dashAbility;

    protected override void Awake()
    {
        base.Awake();
        dashAbility = GetComponent<DashAbility>();
    }

    protected override void Update()
    {
        if (!GameManager.Instance.PlayerControl) return;

        moveDirection = Vector2.zero;
        GetPlayerInput();
        characterBase.SetAnimatorParameters(moveDirection);

        bool dashButton = input.InputActions.Player.DashAttack.triggered;
        if (dashButton)
        {
            isDashButtonDown = true;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (isDashButtonDown)
        {
            Dash();
            isDashButtonDown = false;
        }
    }

    private void GetPlayerInput()
    {
        if (CanMove())
        {
            float moveX = input.MovementInput.x;
            float moveY = input.MovementInput.y;

            moveDirection = new Vector2(moveX, moveY).normalized;
            previousDirection = moveDirection;
        }
    }

    public void Dash()
    {
        dashAbility.Dash(rBody, moveDirection, dashAmount, dashFilter);
    }
}
