using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivateable : BaseActivateable
{
    [SerializeField] private GameObject testSprite;

    public override void Activate()
    {
        base.Activate();
        testSprite.SetActive(true);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        testSprite.SetActive(false);
    }
}
