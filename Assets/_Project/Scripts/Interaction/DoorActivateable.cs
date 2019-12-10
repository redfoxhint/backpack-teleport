using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivateable : MonoBehaviour, IActivateable
{
    [SerializeField] private GameObject testSprite;

    public void Activate()
    {
        testSprite.SetActive(true);
    }

    public void Deactivate()
    {
        testSprite.SetActive(false);
    }
}
