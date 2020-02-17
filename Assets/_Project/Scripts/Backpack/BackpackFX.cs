using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackTeleport.Character.PlayerCharacter;

public class BackpackFX : MonoBehaviour
{
	// Components
	private SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;
	private Animator animator;
	private Animator playerAnimator;
	private TrailRenderer trailRenderer;
	private RipplePostProcessor rippleEffect;
	private Camera cam;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		trailRenderer = GetComponent<TrailRenderer>();
		playerAnimator = FindObjectOfType<Player>().GetComponent<Animator>();
		rippleEffect = FindObjectOfType<RipplePostProcessor>();
		cam = Camera.main;
	}

	void Start()
	{
		trailRenderer.enabled = false;
	}

	public void ShowBackpack()
	{
		spriteRenderer.enabled = true;
		boxCollider.enabled = true;
		animator.SetBool("isFlying", true);
	}

	public void HideBackpack()
	{
		spriteRenderer.enabled = false;
		boxCollider.enabled = false;
		animator.SetBool("isFlying", false);
	}

	public void SwitchHasBackback(bool hasBackpack)
	{
		playerAnimator.SetBool("hasBackpack", hasBackpack);
	}

	public void RippleEffect(Vector3 screenPos)
	{
		Vector3 rippleEffectPos = cam.WorldToScreenPoint(screenPos);
		rippleEffect.RippleAt(rippleEffectPos);
	}

	public void ToggleTrails(bool newState)
	{
		trailRenderer.enabled = newState;
	}
}
