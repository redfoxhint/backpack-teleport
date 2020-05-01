using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using Malee;
using System.Linq;

[System.Serializable]
public class DoorSequenceList : ReorderableArray<BaseActuator> { }

public class DoorActivateable : BaseActivateable
{
    // Inspector Fields
    [Header("Door Activateable Configuration")]
    [SerializeField] private GameObject doorObject; // This is actually the sprite for the door.
    [SerializeField] private List<BaseActuator> actuators; // This list is for storing the activators used to activate the door.
    [SerializeField] private BaseActuator resetActuator = null;

    [Header("Components")]
    [SerializeField] private SpriteRenderer doorSpriteRenderer;


    [Separator]

    [Header("Sequence Config")]
    [SerializeField] private float sequenceResetTime = 2f; // The time the sequence will reset in if a plate is not activated.
    [Reorderable(elementNameOverride = "Plate")] public DoorSequenceList activationSequence; // This list is for storing the correct activation sequence for the door.

    // Private Variables
    private List<BaseActuator> playerSequence = new List<BaseActuator>(); // This list is for storing the sequence that is input by the player during runtime.
    private bool sequenceStarted = false;

    private void Awake()
    {
        foreach (BaseActuator actuator in actuators)
        {
            actuator.OnActivatedEvent += OnActuatorActuated;
            actuator.ActuationType = ActuationType.SEQUENCE;
        }

        if (resetActuator != null)
        {
            resetActuator.OnActivatedEvent += OnResetActivatorActuated;
        }
    }

    public override void OnActuatorActuated(BaseActuator actuator)
    {
        if (!sequenceStarted)
        {
            sequenceStarted = true;
            StartCoroutine(ActivatorSequenceRoutine());
        }

        playerSequence.Add(actuator);
    }

    private IEnumerator ActivatorSequenceRoutine()
    {
        float sequenceTime = sequenceResetTime;
        while (sequenceTime > 0)
        {
            if (sequenceStarted == false) yield break;

            sequenceTime -= Time.deltaTime;
            yield return null;
        }

        CheckIfSequenceCorrect();

        LogUtils.LogWarning("Sequence finished.");

        yield break;
    }

    public override void Reset()
    {
        sequenceStarted = false;

        foreach (BaseActuator actuator in playerSequence)
        {
            actuator.Reset();
        }

        playerSequence.Clear();
        LogUtils.LogWarning("Sequence Reset.");
    }

    private bool CheckIfSequenceCorrect()
    {
        if (CheckGatesOpen())
        {
            if (playerSequence.SequenceEqual(activationSequence))
            {
                LogUtils.Log("SEQUENCE CORRECT!");
                sequenceStarted = false;
                Deactivate();

                return true;
            }

            LogUtils.Log("SEQUENCE INCORRECT!");
            Reset();
        }

        return false;
    }

    public override void OnResetActivatorActuated(BaseActuator actuator)
    {
        if (!IsUnlocked) return;

        Reset();
        Activate();
    }

    public override void Activate()
    {
        base.Activate();
        anim.SetTrigger("closeDoor");
        colliderToDisable.enabled = true;
        IsUnlocked = false;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        anim.SetTrigger("openDoor");
        colliderToDisable.enabled = false;
        IsUnlocked = true;
    }
}
