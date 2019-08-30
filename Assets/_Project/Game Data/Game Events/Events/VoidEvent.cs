using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackTeleport.Events;

[CreateAssetMenu(fileName = "New Void Event", menuName = "Game Events/Void Event")]
public class VoidEvent : BaseGameEvent<Void>
{
    public void Raise() => Raise(new Void());
}
