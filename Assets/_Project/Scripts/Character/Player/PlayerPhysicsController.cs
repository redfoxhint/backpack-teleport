using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPhysicsController : PhysicsCharacterController
{
    [Header("Character Controller Configuration")]
    [SerializeField] private LayerMask dashFilter;
    [SerializeField] private ParticleSystem dustParticles;
    [SerializeField] private float footstepCooldownTime = 0.05f;

    [Header("Dash Configuration")]
    [SerializeField] private float dashAmount;

    private bool isDashButtonDown = false;
    private DashAbility dashAbility;
    private float footstepCooldown;

    private List<AudioFiles> footStepSounds;

    protected override void Awake()
    {
        base.Awake();
        dashAbility = GetComponent<DashAbility>();

        footStepSounds = new List<AudioFiles> { AudioFiles.SFX_GrassWalk2, AudioFiles.SFX_GrassWalk3, AudioFiles.SFX_GrassWalk4 };
    }

    protected override void Update()
    {
        footstepCooldown -= Time.deltaTime;

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

            if(moveX != 0 || moveY != 0)
            {
                Footsteps();
            }

            moveDirection = new Vector2(moveX, moveY).normalized;
            previousDirection = moveDirection;
        }
    }

    public void Dash()
    {
        dashAbility.Dash(rBody, moveDirection, dashAmount, dashFilter);
    }

    private void Footsteps()
    {
        int index = Random.Range(0, footStepSounds.Count);

        if(footstepCooldown < 0f)
        {
            AudioManager.Instance.PlaySoundEffect(footStepSounds[index], 0.2f);
            footstepCooldown = footstepCooldownTime;
        }
    }
}
