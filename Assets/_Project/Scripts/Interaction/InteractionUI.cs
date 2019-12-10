using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class InteractionUI : MonoBehaviour
{
	// Inspector Fields
	[Header("Interaction UI Settings")]
	[SerializeField] private TextMeshProUGUI interactionText;

	// Private Variables
	private Animator animator;
	private bool isOpen;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		interactionText = GetComponentInChildren<TextMeshProUGUI>();
	}

	public void EnableInteractionUI(InteractableData objectData)
	{
		if (!isOpen)
		{
			isOpen = true;
			interactionText.SetText("F to " + objectData.interactionText);
			animator.SetBool("IsOpen", true);
		}
	}

	public void DisableInteractionUI()
	{
		if (isOpen)
		{
			isOpen = false;
			animator.SetBool("IsOpen", false);
		}
	}
}
