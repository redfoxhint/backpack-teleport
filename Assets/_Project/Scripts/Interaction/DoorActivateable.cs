using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MyBox;
using Malee;
using System.Linq;

public enum ActivateableMode { SINGLE, SEQUENCE }

[System.Serializable]
public class DoorActivatorsList : ReorderableArray<PressurePlateActivateable> { }

public class DoorActivateable : BaseActivateable
{
    // Inspector Fields
    [SerializeField] private ActivateableMode doorMode;
    [SerializeField] private GameObject door;
    [SerializeField] private bool isLocked = true;

    [Header("Sequence Config")]
    [SerializeField] private PressurePlateActivateable resetPlate = null;
    [SerializeField] private float sequenceResetTime = 2f; // The time the sequence will reset in if a plate is not activated.

    [Header("Components")]
    [SerializeField] private SpriteRenderer doorSpriteRenderer;

    [Separator]

    [SerializeField] private List<PressurePlateActivateable> pressurePlates;
    [Reorderable(elementNameOverride = "Plate")] public DoorActivatorsList doorActivators;

    // Private Variables
    [SerializeField] private List<PressurePlateActivateable> sequencePlates = new List<PressurePlateActivateable>();
    private bool sequenceStarted = false;

    private void Awake()
    {
        foreach (PressurePlateActivateable plate in pressurePlates)
        {
            plate.OnActivatedEvent += OnPlateActivated;
            plate.IsPartOfSequence = true;
        }
    }

    private void OnPlateActivated(PressurePlateActivateable plate)
    {
        if (!sequenceStarted)
        {
            sequenceStarted = true;
            StartCoroutine(PlateSequenceRoutine());
        }

        if(plate.IsDefault)
        {
            Reset();
            Activate();
        }
        else
        {
            sequencePlates.Add(plate);
        }

        //if (plate.IsDefault)
        //{
        //    CheckIfSequenceCorrect();
        //    return;
        //}
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
            //Reset();
            return true;
        }

        LogUtils.Log("SEQUENCE INCORRECT!");
        Reset();

        return false;
    }

    public override void Activate()
    {
        base.Activate();
        door.SetActive(true);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        door.SetActive(false);
    }
}
