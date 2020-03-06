using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
	// Public Variables

	// Private Variables

	// Components
	private Animator animator;

	// Effect Prefabs

	private void Awake()
	{
        animator = GetComponent<Animator>();
	}

    public void SwitchHasBackback(bool hasBackpack)
	{
		animator.SetBool("hasBackpack", true);
	}

	public void TriggerThrowing()
	{
		animator.SetTrigger("Throw");
	}
}
