using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractable : Interactable
{
	// Inspector Fields
	[Header("Interactable Object Data")]
	[SerializeField] protected InteractableData objectData;

	// Private Variables
	protected InteractionUI interactionUI;

	private void Awake()
	{
		interactionUI = GetComponentInChildren<InteractionUI>();
	}

	public override void Interact()
	{

	}

	public virtual void EnableUI()
	{
		//if (isInteractable)
		//{
		//	interactionUI.EnableInteractionUI(objectData);
		//}
	}

	public virtual void DisableUI()
	{
		interactionUI.DisableInteractionUI();
	}
}
