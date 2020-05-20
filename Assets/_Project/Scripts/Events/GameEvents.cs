using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents
{
    public static UnityEventOnBackpackThrown onBackpackThrownEvent = new UnityEventOnBackpackThrown();
    public static UnityOnTeleportedEvent onTeleportedEvent = new UnityOnTeleportedEvent();
    public static UnityOnEntityKilledEvent onEntityKilled = new UnityOnEntityKilledEvent();
    public static UnityOnQuestAssigned onQuestAssigned = new UnityOnQuestAssigned();
    public static UnityOnQuestUpdated onQuestUpdated = new UnityOnQuestUpdated();
    public static UnityOnQuestCompleted onQuestCompleted = new UnityOnQuestCompleted();
    public static UnityOnNotificationCreated onNotificationCreated = new UnityOnNotificationCreated();
    public static UnityOnSceneLoaded onSceneLoaded = new UnityOnSceneLoaded();
}

public class UnityEventOnBackpackThrown : UnityEvent { }
public class UnityOnTeleportedEvent : UnityEvent<Vector2> { }
public class UnityOnEntityKilledEvent : UnityEvent<EntityType> { }
public class UnityOnQuestAssigned : UnityEvent<Quest> { }
public class UnityOnQuestUpdated : UnityEvent<Quest> { }
public class UnityOnQuestCompleted : UnityEvent<Quest> { }
public class UnityOnNotificationCreated : UnityEvent<Notification> { }
public class UnityOnSceneLoaded : UnityEvent { }


