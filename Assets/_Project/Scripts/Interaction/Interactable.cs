using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] protected GameObject interactableUI;
    [SerializeField] private KeyCode interactionKeyCode = KeyCode.E;
    [SerializeField] private bool isInteractable = true;

    // Private variables
    protected bool isInRange = false;

    private InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    protected virtual void Start()
    {
        if(interactableUI != null)
        {
            interactableUI.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        if(isInRange && InputManager.Instance.KeyDown("interact"))
        {
            Interact();
            OnOutOfRange();
        }
    }

    public abstract void Interact();

    protected virtual void OnInRange()
    {
        interactableUI.SetActive(true);
    }

    protected virtual void OnOutOfRange()
    {
        interactableUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GetCollision(other.gameObject, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GetCollision(other.gameObject, false);
    }

    private void GetCollision(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            isInRange = state;

            if (state == true)
            {
                OnInRange();
            }
            else if (state == false)
            {
                OnOutOfRange();
            }
        }
    }

}
