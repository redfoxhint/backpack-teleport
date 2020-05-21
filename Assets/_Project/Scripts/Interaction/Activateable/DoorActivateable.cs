using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using Malee;
using System.Linq;
using System;

[System.Serializable]
public class DoorSequenceList : ReorderableArray<BaseActuator> { }

public class DoorActivateable : BaseActivateable
{
    // Inspector Fields
    [Header("Door Activateable Configuration")]
    [SerializeField] private GameObject doorObject; // This is actually the sprite for the door.
    [SerializeField] private List<BaseActuator> actuators; // This list is for storing the activators used to activate the door.
    [SerializeField] private BaseActuator resetActuator = null;
    [SerializeField] private bool openAutomaticallyAfterAllGatesOpen = false;
    [SerializeField] private bool closeDoorAfterActuatorDeactivate = false;
    [SerializeField] private AudioFiles doorOpenSound;

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
        if(activationMode == ActivateableMode.SEQUENCE)
        {
            foreach (BaseActuator actuator in actuators)
            {
                actuator.OnActivatedEvent += OnActuatorActuated;
                actuator.ActuationType = ActuationType.SEQUENCE;
            }
        }

        else if(activationMode == ActivateableMode.MULTIPLE)
        {
            foreach (BaseActuator actuator in actuators)
            {
                actuator.OnActivatedEvent += OnActuatorActuated;
                actuator.OnDeactivatedEvent += OnActuatorDeactivated;
                actuator.ActuationType = ActuationType.SINGLE;
            }
        }

        else
        {
            if(singleActuator != null)
            {
                singleActuator.OnActivatedEvent += OnActuatorActuated;
                singleActuator.OnDeactivatedEvent += OnActuatorDeactivated;
            }
            else
            {
                LogUtils.LogError($"Actuator not found. Make sure you assign a single actuator.");
            }
        }

        if (resetActuator != null)
        {
            resetActuator.OnActivatedEvent += OnResetActivatorActuated;
        }
    }

   

    private void Update()
    {
        if (IsUnlocked) return;

        if (isGated && openAutomaticallyAfterAllGatesOpen)
        {
            if(CheckGatesOpen())
            {
                Deactivate();
            }
        }
    }

    public override void OnActuatorActuated(BaseActuator actuator)
    {
        if (IsUnlocked) return;

        if(activationMode == ActivateableMode.SINGLE)
        {
            if(CheckGatesOpen())
            {
                Deactivate();
            }

            return;
        }

        if(activationMode == ActivateableMode.MULTIPLE)
        {
            if(CheckGatesOpen())
            {
                Deactivate();
            }

            return;
        }

        if (!sequenceStarted)
        {
            sequenceStarted = true;
            StartCoroutine(ActivatorSequenceRoutine());
        }

        playerSequence.Add(actuator);
    }

    public override void OnActuatorDeactivated(BaseActuator actuator)
    {
        if(closeDoorAfterActuatorDeactivate)
        {
            bool allDeactuated = actuators.Any() && actuators.All(item => !item.IsActuated);

            if(allDeactuated)
            {
                Activate();
            }
        }
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
        anim.SetBool("openDoor", false);
        colliderToDisable.enabled = true;
        IsUnlocked = false;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        anim.SetBool("openDoor", true);
        AudioManager.Instance.PlaySoundEffect(doorOpenSound);
        colliderToDisable.enabled = false;
        IsUnlocked = true;
    }
}
