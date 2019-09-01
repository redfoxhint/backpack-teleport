using System.Collections;
using UnityEngine;
using BackpackTeleport.Character;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterDash : MonoBehaviour
{
	[Header("Dash Setup")]
	[SerializeField] private float dashSpeed = 5f;
	[SerializeField] private float startDashTime;
	[SerializeField] private DashAimType aimType = DashAimType.MoveDirection;

	// Private Variables
	private float dashTime;
	private float startDrag;
	private int direction;
	private bool hasDashed;
	private bool isDashing;
	private enum DashAimType { MouseDirection, MoveDirection }

	private BaseCharacterMovement characterMovement;

	// Components
	private Rigidbody2D rBody2D;

	private void Awake()
	{
		rBody2D = GetComponent<Rigidbody2D>();
		characterMovement = GetComponent<BaseCharacterMovement>();
		dashTime = startDashTime;
	}

	private void Start()
	{
		startDrag = characterMovement.RBody2D.drag;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if (characterMovement.Velocity.sqrMagnitude != 0 && !hasDashed)
			{
				Dash(characterMovement.Velocity.x, characterMovement.Velocity.y);
			}
		}
	}

	private void Dash(float x, float y)
	{
		hasDashed = true;
		rBody2D.velocity = Vector2.zero;

		Vector2 dashDirection = Vector2.zero;

		switch (aimType)
		{
			case DashAimType.MouseDirection:
				Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				dashDirection = (mousePos - (Vector2)transform.position).normalized;
				break;

			case DashAimType.MoveDirection:
				dashDirection = new Vector2(x, y);
				break;
		}

		//Vector2 dashDirection = new Vector2(x, y);
		rBody2D.AddForce(dashDirection.normalized * dashSpeed);
		StartCoroutine(DashWait());
		Debug.Log("Dashed");
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
		rBody2D.drag = drag;
	}
}
