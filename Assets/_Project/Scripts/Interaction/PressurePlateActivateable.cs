using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlateColor { GREY, RED, GREEN, BLUE }

public class PressurePlateActivateable : BaseActivateable
{
    [System.Serializable]
    struct RuneGraphics
    {
        public Sprite deactivatedRuneSprite;
        public Sprite activatedRuneSprite;
        public Sprite pressedRuneSprite;

        public void OnActivated(SpriteRenderer spr)
        {
            spr.sprite = pressedRuneSprite;
        }

        public void OnDeactivated(SpriteRenderer spr)
        {
            spr.sprite = activatedRuneSprite;
        }
    }

    // Inspector Variables
    [Header("Configuration")]
    [SerializeField] private Sprite activatedPlateSprite;
    [SerializeField] private Sprite deactivatedPlateSprite;
    [SerializeField] private RuneGraphics runeGraphics;
    [SerializeField] private PlateColor plateColor;


    [SerializeField] private BaseActivateable objectToActivate;
    [SerializeField] private List<IActivator> currentActivators = new List<IActivator>();

    [Header("Components")]
    [SerializeField] private SpriteRenderer plateSpr;
    [SerializeField] private SpriteRenderer runeSpr;

    // Properties
    //public int PlateID { get => plateID; }
    public PlateColor PlateColor { get => plateColor; }
    private ActivateableMode ActivateableMode { get => activateableMode; set => value = activateableMode; }
    public bool IsDefault { get => plateColor == PlateColor.GREY; }

    public bool IsPartOfSequence
    {
        set
        {
            activateableMode = ActivateableMode.SEQUENCE;
        }
    }

    // Private Variables
    private bool isDisabled;
    private ActivateableMode activateableMode = ActivateableMode.SINGLE;

    private void Awake()
    {
        InitPlate();
    }

    private void InitPlate()
    {
        if (plateSpr == null || runeSpr == null)
        {
            LogUtils.LogError("Sprite Renderer for either the plate or the rune was not found.");
            return;
        }

        if (!isDisabled)
        {
            plateSpr.sprite = deactivatedPlateSprite;

            if (!IsDefault)
                runeSpr.sprite = runeGraphics.activatedRuneSprite;
        }
    }

    public override void Actuate()
    {
        if (Actuated) return;

        switch (activateableMode)
        {
            case ActivateableMode.SINGLE:
                ActivateModeSingle();
                break;
            case ActivateableMode.SEQUENCE:
                ActivateModeSequence();
                break;
        }
    }

    public override void Deactivate()
    {
        switch (activateableMode)
        {
            case ActivateableMode.SINGLE:
                DeactivateModeSingle();
                break;
            case ActivateableMode.SEQUENCE:
                DeactivateModeSequence();
                break;
        }
    }

    private void ActivateModeSequence()
    {
        SwitchGraphics(true);
        OnActuatedEvent?.Invoke(this);
        Actuated = true;
    }

    private void ActivateModeSingle()
    {
        SwitchGraphics(true);
        OnActuatedEvent?.Invoke(this);
        Actuated = true;

        if (objectToActivate != null)
        {
            objectToActivate.Deactivate();
        }
    }

    private void DeactivateModeSingle()
    {
        SwitchGraphics(false);
        Actuated = false;
    }

    private void DeactivateModeSequence()
    {

    }

    private void TriggerModeSingleEnter(Collider2D other)
    {
        IActivator activator = other.GetComponent<IActivator>();

        if (activator != null)
        {
            if (!currentActivators.Contains(activator))
            {
                currentActivators.Add(activator);
            }

            Actuate();
        }
    }

    private void TriggerModeSingleExit(Collider2D other)
    {
        IActivator activator = other.GetComponent<IActivator>();

        if (activator != null)
        {
            if (currentActivators.Contains(activator))
            {
                currentActivators.Remove(activator);
            }

            if (currentActivators.Count == 0)
            {
                Deactivate();
            }
        }
    }

    private void TriggerModeSequenceEnter(Collider2D other)
    {
        IActivator activator = other.GetComponent<IActivator>();

        if (activator != null)
        {
            if (!currentActivators.Contains(activator))
            {
                currentActivators.Add(activator);
            }

            Actuate();
        }
    }

    private void TriggerModeSequenceExit(Collider2D other)
    {

    }

    private void SwitchGraphics(bool toggle)
    {
        if(toggle)
        {
            plateSpr.sprite = activatedPlateSprite;
            runeGraphics.OnActivated(runeSpr);
        }
        else
        {
            plateSpr.sprite = deactivatedPlateSprite;
            runeGraphics.OnDeactivated(runeSpr);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(activateableMode)
        {
            case ActivateableMode.SINGLE:
                TriggerModeSingleEnter(other);
                break;
            case ActivateableMode.SEQUENCE:
                TriggerModeSequenceEnter(other);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsDefault) DeactivateModeSingle();

        switch(activateableMode)
        {
            case ActivateableMode.SINGLE:
                TriggerModeSingleExit(other);
                break;
            case ActivateableMode.SEQUENCE:
                TriggerModeSequenceExit(other);
                break;
        }
    }

    public void Reset()
    {
        Actuated = false;
        SwitchGraphics(false);
    }
}
