using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using Malee;
using System.Linq;

[System.Serializable]
public class DoorActivatorsList : ReorderableArray<BaseActivateable> { }

public class DoorActivateable : BaseActivateable
{
    // Inspector Fields
    [SerializeField] private GameObject doorObject; // This is actually the sprite for the door.

    [Header("Sequence Config")]
    [SerializeField] private PressurePlateActivateable resetPlate = null;
    [SerializeField] private float sequenceResetTime = 2f; // The time the sequence will reset in if a plate is not activated.

    [Header("Components")]
    [SerializeField] private SpriteRenderer doorSpriteRenderer;

    [Separator]

    [SerializeField] private List<BaseActivateable> pressurePlates;
    [Reorderable(elementNameOverride = "Plate")] public DoorActivatorsList doorActivators;

    // Private Variables
    [SerializeField] private List<BaseActivateable> sequencePlates = new List<BaseActivateable>();
    private bool sequenceStarted = false;

    private void Awake()
    {
        foreach (PressurePlateActivateable plate in pressurePlates)
        {
            plate.OnActuatedEvent += OnPlatePressed;
            plate.IsPartOfSequence = true;
        }

        if(resetPlate != null)
        {
            resetPlate.OnActuatedEvent += OnResetPlatePressed;
        }
    }

    private void OnPlatePressed(BaseActivateable plate)
    {
        PressurePlateActivateable pressurePlate = plate as PressurePlateActivateable;
        if (pressurePlate.IsDefault) return;

        if (!sequenceStarted)
        {
            sequenceStarted = true;
            StartCoroutine(PlateSequenceRoutine());
        }

        sequencePlates.Add(plate);
    }

    private IEnumerator PlateSequenceRoutine()
    {
        float sequenceTime = sequenceResetTime;
        while(sequenceTime > 0)
        {
            if (sequenceStarted == false) yield break;

            sequenceTime -= Time.deltaTime;
            yield return null;
        }

        CheckIfSequenceCorrect();
        
        LogUtils.LogWarning("Sequence finished.");

        yield break;
    }

    private void Reset()
    {
        sequenceStarted = false;

        foreach(PressurePlateActivateable plate in sequencePlates)
        {
            plate.Reset();
        }

        sequencePlates.Clear();
        LogUtils.LogWarning("Sequence Reset.");
    }

    private bool CheckIfSequenceCorrect()
    {
        if (sequencePlates.SequenceEqual(doorActivators))
        {
            LogUtils.Log("SEQUENCE CORRECT!");
            Deactivate();

            return true;
        }

        LogUtils.Log("SEQUENCE INCORRECT!");
        Reset();

        return false;
    }

    private void OnResetPlatePressed(BaseActivateable baseActivateable)
    {
        Reset();
        Actuate();
    }

    public override void Actuate()
    {
        base.Actuate();
        doorObject.SetActive(true);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        doorObject.SetActive(false);
    }
}
