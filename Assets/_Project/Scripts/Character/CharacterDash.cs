using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CharacterDash : MonoBehaviour
{
    [Header("Dash Setup")]
    [SerializeField] private float dashSpeed = 5f;
    [SerializeField] private DashAimType aimType = DashAimType.MoveDirection;

    // Private Variables
    private float startDrag;
    private bool hasDashed;
    private bool isDashing;
    private enum DashAimType { MouseDirection, MoveDirection }

    // Components
    private Rigidbody2D rBody;

    public void Dash(Vector2 direction, Rigidbody2D rBody)
    {
        if (!CanDash(direction)) return;

        this.rBody = rBody;
        startDrag = rBody.drag;

        hasDashed = true;
        rBody.velocity = Vector2.zero;

        Vector2 dashDirection = Vector2.zero;

        switch (aimType)
        {
            case DashAimType.MouseDirection:
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dashDirection = (mousePos - (Vector2)transform.position).normalized;
                break;

            case DashAimType.MoveDirection:
                dashDirection = new Vector2(direction.x, direction.y);
                break;
        }

        rBody.AddForce(dashDirection.normalized * dashSpeed);
        StartCoroutine(DashWait());
        Debug.Log("Dashed");
    }

    private bool CanDash(Vector2 direction)
    {
        return direction.sqrMagnitude != 0 && !hasDashed;
    }

    IEnumerator DashWait()
    {
        FindObjectOfType<GhostingEffect>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, startDrag, 0.8f, SetRigidbodyDrag);
        isDashing = true;

        yield return new WaitForSeconds(0.3f);
        isDashing = false;
        yield break;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(0.15f);
        hasDashed = false;
        yield break;
    }

    private void SetRigidbodyDrag(float drag)
    {
        rBody.drag = drag;
    }
}
