using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MyBox;
using Malee;

public enum ActuataeableMode { SEQUENCE, SINGLE }

[System.Serializable]
public class ActuateableSequenceList : ReorderableArray<Actuataeable> { }

public class Actuataeable : MonoBehaviour, IActuateable
{
    /*
     * 1) Enum for sequence actuated or single actuated. (Requires multiple actuators for sequence)
     * 2) bool isGated for knowing if the door is gated by other actuators. (If the door requires previous doors to be unlocked)
     * 3) List for keeping track of gates. (Other actuateables)
     */

    [Header("Actuateable Configuration")]
    [SerializeField] private ActuataeableMode actuataeableMode;
    [SerializeField] private bool isGated = false;
    [Tooltip("If the door is gated, then this list is where you put the other actuateables")]
    [SerializeField] private List<Actuataeable> gates = new List<Actuataeable>();
    [Tooltip("Actuator actuables go here")]
    [SerializeField] private List<Actuataeable> actuators = new List<Actuataeable>();

    [Header("Sequence Settings")]
    [SerializeField] private float sequenceResetTime;
    [Reorderable(elementNameOverride = "Actuator")] public ActuateableSequenceList sequenceActuateables;

    [Header("Components")]
    [SerializeField] private Animator animator;

    // Private Variables
    private bool isActuated = false;

    #if UNITY_EDITOR
    // Called in editor script.
    public void PopulateSequence()
    {
        sequenceActuateables.Clear();

        foreach(Actuataeable actuator in actuators)
        {
            sequenceActuateables.Add(actuator);
        }
    }
    #endif
}

