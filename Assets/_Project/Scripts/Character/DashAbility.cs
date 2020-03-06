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
    [SerializeField] private float ghostAmount;

    [Header("Dash Configuration")]
    [SerializeField] private float dashTime = 1f; // The amount of time it will take to complete the dash (dash speed).
    [SerializeField] private float dashAmount = 5f;

    // Private Variables
    private Vector2 dash;
    private bool isDashing = false;

    // Dependencies
    private GhostingEffect ghostingEffect;

    public void Dash(BaseObjectMovement baseObjectMovement, System.Action OnDashCompleteCallback)
    {
        if (isDashing) return;

        StartCoroutine(DashRoutine(baseObjectMovement, OnDashCompleteCallback));
    }

    //public void Dash(BaseObjectMovement baseObjectMovement, System.Action OnDashCompleteCallback)
    //{
    //    if (isDashing) return;

    //    // Show the ghosting effect
    //    ghostingEffect = new GhostingEffect(this.gameObject, ghostTrailColor, ghostFadeColor, ghostInterval, ghostFadeTime, ghostAmount);
    //    ghostingEffect.ShowGhost();

    //    isDashing = true;

    //    Vector2 dashDirection = new Vector2(Mathf.RoundToInt(baseObjectMovement.Velocity.x), Mathf.RoundToInt(baseObjectMovement.Velocity.y)).normalized;
    //    dash = dashDirection * dashAmount;
    //    Debug.Log(dash);
    //    Vector2 beforeDashPos = transform.position;

    //    DOTween.To(() => dash, x => dash = x, Vector2.zero, dashTime).OnComplete(
    //        () => 
    //        { 
    //            isDashing = false;
    //            Vector2 afterDashPos = transform.position;
    //            //DashTest(beforeDashPos, afterDashPos);
    //            OnDashCompleteCallback(); 
    //            ghostingEffect = null; 
    //        });

    //    baseObjectMovement.TargetVelocity = dash;
    //}

    private IEnumerator DashRoutine(BaseObjectMovement baseObjectMovement, System.Action OnDashCompleteCallback)
    {
        isDashing = true;
        float elapsedTime = 0;

        // Show the ghosting effect
        ghostingEffect = new GhostingEffect(this.gameObject, ghostTrailColor, ghostFadeColor, ghostInterval, ghostFadeTime, ghostAmount);
        ghostingEffect.ShowGhost();

        Vector2 dashDirection = new Vector2(Mathf.RoundToInt(baseObjectMovement.Velocity.x), Mathf.RoundToInt(baseObjectMovement.Velocity.y)).normalized;
        dash = dashDirection * dashAmount;

        Vector2 beforeDashPos = transform.position;

        while (elapsedTime < dashTime)
        {
            baseObjectMovement.TargetVelocity = dash;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Vector2 afterDashPos = transform.position;
        DashTest(beforeDashPos, afterDashPos);

        OnDashCompleteCallback();
        isDashing = false;
        yield break;
    }

    private void DashTest(Vector2 beforeDashPos, Vector2 afterDashPos)
    {
        float distanceTravelled = Vector2.Distance(beforeDashPos, afterDashPos);
        Debug.Log($"Distance travelled after dashing: {distanceTravelled}");
    }
}
