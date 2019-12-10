using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInteractions : MonoBehaviour
{
	// Inspector Fields
	[Header("Interaction Configuration")]
	[SerializeField] private float maxInteractionDistance;
	[SerializeField] private float interactionCircleRadius = 1.2f;
	[SerializeField] private Transform interactionCircleOrigin;
	[SerializeField] private KeyCode interactionKey = KeyCode.F;
	[SerializeField] private LayerMask interactableLayer;

	// Private Variables
	private bool canInteract = false;
	private ObjectInteractable lastItemInteracted;
	private InputManager inputManager;

	private void OnEnable()
	{
		inputManager = InputManager.Instance;
	}

	private void Update()
	{
		GetInteraction();

		if (Input.GetKeyDown(interactionKey))
		{
			if (canInteract && lastItemInteracted != null)
			{
				lastItemInteracted.Interact();
			}
		}
	}

	private void GetInteraction()
	{
		Collider2D[] interactable = Physics2D.OverlapCircleAll(transform.position, interactionCircleRadius, interactableLayer);

		foreach (Collider2D col in interactable)
		{
			ObjectInteractable detectedInteractable = col.GetComponent<ObjectInteractable>();

			if (detectedInteractable != null)
			{
				canInteract = true;
				lastItemInteracted = detectedInteractable;
                lastItemInteracted.EnableUI();
			}
		}

        if (lastItemInteracted != null)
		{
			float distanceToInteractable = GetDistance(lastItemInteracted.transform.position);

			if (distanceToInteractable >= maxInteractionDistance)
			{
				canInteract = false;
                lastItemInteracted.DisableUI();
				lastItemInteracted = null;
			}
		}
	}

	private float GetDistance(Vector2 objectPos)
	{
		return Vector2.Distance(transform.position, objectPos);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, interactionCircleRadius);
	}
}
