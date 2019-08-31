using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterDash : MonoBehaviour
{
	[Header("Dash Setup")]
	[SerializeField] private float dashSpeed = 5f;
	[SerializeField] private float startDashTime;

	// Private Variables
	private float dashTime;
	private int direction;
	private bool hasDashed;
	private bool isDashing;

	private PlayerMovement playerMovement;

	// Components
	private Rigidbody2D rBody2D;

	private void Awake()
	{
		rBody2D = GetComponent<Rigidbody2D>();
		playerMovement = GetComponent<PlayerMovement>();
		dashTime = startDashTime;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if (playerMovement.Movement.sqrMagnitude != 0 && !hasDashed)
			{
				Dash(playerMovement.Movement.x, playerMovement.Movement.y);
			}
		}
	}

	private void Dash(float x, float y)
	{
		hasDashed = true;
		rBody2D.velocity = Vector2.zero;
		Vector2 dashDirection = new Vector2(x, y);
		rBody2D.AddForce(dashDirection.normalized * dashSpeed);
		StartCoroutine(DashWait());
        Debug.Log("Dashed");
	}

	IEnumerator DashWait()
	{
		FindObjectOfType<GhostingEffect>().ShowGhost();
		StartCoroutine(GroundDash());
		DOVirtual.Float(14, 0, 0.8f, SetRigidbodyDrag);
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
		rBody2D.drag = drag;
	}
}
