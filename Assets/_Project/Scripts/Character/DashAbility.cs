using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DashState
{
    READY,
    DASHING,
    COOLDOWN
}

public class DashAbility : MonoBehaviour
{
    [SerializeField] private DashState dashState;
    [SerializeField] private float dashTimer;
    [SerializeField] private float maxDash = 20f;
    [SerializeField] private Vector2 savedVelocity;

    private Rigidbody2D rBody;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch(dashState)
        {
            case DashState.READY:

                bool isDashKeyDown = Input.GetKeyDown(KeyCode.Space);
                if(isDashKeyDown)
                {
                    savedVelocity = rBody.velocity;
                    rBody.velocity = new Vector2(rBody.velocity.x * 5f, rBody.velocity.y);
                    dashState = DashState.DASHING;
                }

                break;

            case DashState.DASHING:

                dashTimer += Time.deltaTime * 3f;

                if(dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    rBody.velocity = savedVelocity;
                    dashState = DashState.COOLDOWN;
                }

                break;

            case DashState.COOLDOWN:

                dashTimer -= Time.deltaTime;

                if(dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.READY;
                    Debug.Log("Cooldown Finished");
                }

                break;
        }
    }
}
