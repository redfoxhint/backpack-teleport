using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateActivateable : BaseActivateable
{
    // Inspector Variables
    [SerializeField] private BaseActivateable objectToActivate;

    // Private Variables
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Activate()
    {
        animator.SetBool("isPressed", true);

        if(objectToActivate != null)
        {
            objectToActivate.Deactivate();
        }
    }

    public override void Deactivate()
    {
        animator.SetBool("isPressed", false);

        if (objectToActivate != null)
        {
            objectToActivate.Activate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IActivator activator = other.GetComponent<IActivator>();

        if(activator != null)
        {
            Activate();
            Debug.Log("Activated");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IActivator activator = other.GetComponent<IActivator>();

        if (activator != null)
        {
            Deactivate();
            Debug.Log("Deactivated");
        }
    }
}
