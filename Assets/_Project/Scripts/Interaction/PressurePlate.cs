using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IActivateable
{
    // Private Variables
    private Animator animator;

    [SerializeField] private DoorActivateable doorActivateable;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        animator.SetBool("isPressed", true);

        if(doorActivateable != null)
        {
            doorActivateable.Deactivate();
        }

    }

    public void Deactivate()
    {
        animator.SetBool("isPressed", false);

        if (doorActivateable != null)
        {
            doorActivateable.Activate();
        }
    }
}
