using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterDash))]
public class PlayerMovement : CharacterMovement
{
    // Inspector Fields

    // Private Variables

    // Properties

    // Components
    private CharacterDash characterDash;
    private float defaultDashAmount = 5f;

    private Vector2 vel;

    protected override void Awake()
    {
        base.Awake();

        characterDash = GetComponent<CharacterDash>();
    }
    protected override void Update()
    {
        GetMovementInput();
    }

    private void FixedUpdate()
    {
        rBody.MovePosition(rBody.position + vel * MoveSpeed * Time.fixedDeltaTime);
    }

    private void GetMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        vel = new Vector2(horizontal, vertical);
        vel = Vector2.ClampMagnitude(vel, 1);
    }

    private void Dash(Vector2 direction)
    {
        characterDash.Dash(direction, rBody, defaultDashAmount);
    }

    public void DashInLastMovementDirection(float amount)
    {
        characterDash.Dash(LastVelocity, rBody, amount);
    }
}
