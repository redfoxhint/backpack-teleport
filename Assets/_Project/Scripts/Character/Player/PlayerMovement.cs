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

    protected override void Awake()
    {
        base.Awake();

        characterDash = GetComponent<CharacterDash>();
    }
    protected override void Update()
    {
        Movement();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Dash(MovementVector);
        }
    }
    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 vel = new Vector2(horizontal, vertical);
        vel = Vector2.ClampMagnitude(vel, 1);

        Move(vel);
    }

    private void Dash(Vector2 direction)
    {
        characterDash.Dash(direction, rBody);
    }
}
