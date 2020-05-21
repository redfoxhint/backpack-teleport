using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : BaseActuator
{
    [System.Serializable]
    struct PressurePlateGraphics
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

    [Header("Pressure Plate Configuration")]
    [SerializeField] private PressurePlateGraphics plateGraphics;
    [SerializeField] private SpriteRenderer runeSpriteRenderer;
    [SerializeField] private bool hasNoRune;
    [SerializeField] private UnityEvent onPressurePlateActuated;
    public int id;

    // Private Variables
    private List<IActivator> currentActivators = new List<IActivator>();

    private void Awake()
    {
        if (actuatorSpriteRenderer == null || runeSpriteRenderer == null)
        {
            LogUtils.LogError("Sprite Renderer for either the plate or the rune was not found.");
            return;
        }

        if (!isDisabled)
        {
            actuatorSpriteRenderer.sprite = deactivatedSprite;

            if (!hasNoRune)
                runeSpriteRenderer.sprite = plateGraphics.activatedRuneSprite;
        }
    }

    public override void Activate()
    {
        SwitchGraphics(true);
        OnActivatedEvent?.Invoke(this);

        if (ActuationType == ActuationType.SINGLE)
        {
            ObjectToActivate?.Deactivate();
        }
    }

    public override void Deactivate()
    {
        if (ActuationType == ActuationType.SINGLE)
        {
            SwitchGraphics(false);
            OnDeactivatedEvent?.Invoke(this);
            ObjectToActivate?.Activate();
        }
    }

    private void SwitchGraphics(bool toggle)
    {
        if (toggle)
        {
            actuatorSpriteRenderer.sprite = activatedSprite;
            plateGraphics.OnActivated(runeSpriteRenderer);
        }
        else
        {
            actuatorSpriteRenderer.sprite = deactivatedSprite;
            plateGraphics.OnDeactivated(runeSpriteRenderer);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IActivator activator = other.GetComponent<IActivator>();

        if (activator != null)
        {
            if (!currentActivators.Contains(activator))
            {
                currentActivators.Add(activator);
            }

            if (IsActuated) return;
            IsActuated = true;
            GameEvents.onPressurePlateActuated.Invoke(this);

            LogUtils.Log("Stepped on pressure plate.");
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (stayActuated) return;

        IActivator activator = other.GetComponent<IActivator>();

        if (activator != null)
        {
            if (currentActivators.Contains(activator))
            {
                currentActivators.Remove(activator);
            }
        }

        if (currentActivators.Count == 0)
        {
            LogUtils.Log("Left pressure plate");
            IsActuated = false;

            Deactivate();
        }
    }

    public override void Reset()
    {
        SwitchGraphics(false);
        IsActuated = false;
    }
}
