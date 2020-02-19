using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents
{
    public static UnityEventOnBackpackThrown onBackpackThrownEvent = new UnityEventOnBackpackThrown();
    public static UnityOnTeleportedEvent onTeleportedEvent = new UnityOnTeleportedEvent();
}

public class UnityEventOnBackpackThrown : UnityEvent<Backpack> { }
public class UnityOnTeleportedEvent : UnityEvent<Vector2> { }


