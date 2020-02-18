using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents
{
    public static CustomTestEvent testEvent = new CustomTestEvent();
    public static UnityEventOnBackpackThrown onBackpackThrownEvent = new UnityEventOnBackpackThrown();
}

public class CustomTestEvent : UnityEvent<CustomTestEventData> { }
public class UnityEventLevelData : UnityEvent<LevelData> { }
public class UnityEventOnBackpackThrown : UnityEvent<Backpack> { }

public class CustomTestEventData
{
    public Transform pos;

    public CustomTestEventData(Transform pos)
    {
        this.pos = pos;
    }
}


