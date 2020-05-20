using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DashAbility : MonoBehaviour
{
    [Header("Ghost Effect Configuration")]
    [SerializeField] private Color ghostTrailColor;
    [SerializeField] private Color ghostFadeColor;
    [SerializeField] private float ghostInterval;
    [SerializeField] private float ghostFadeTime;
    [SerializeField] private int ghostAmount;

    // Private Variables
    private Vector2 dash;
    private bool isDashing = false;

    // Dependencies
    private GhostingEffect ghostingEffect;

    public void Dash(Rigidbody2D rBody, Vector2 dashDirection, float dashAmount, LayerMask dashFilter)
    {
        Vector2 dashPosition = (Vector2)transform.position + dashDirection.normalized * dashAmount;

        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dashDirection, dashAmount, dashFilter);
        if (raycastHit2D.collider != null)
        {
            dashPosition = raycastHit2D.point;
        }

        // Do Dash Effect here
        ghostingEffect = GhostingEffect.CreateEffect(this.gameObject, ghostTrailColor, ghostFadeColor, ghostFadeTime, ghostInterval, ghostAmount);
        ghostingEffect.ShowGhost();

        rBody.MovePosition(dashPosition);
    }
}
