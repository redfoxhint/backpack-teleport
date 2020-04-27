using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchActuator : BaseActuator
{
    [Header("Switch Actuator Configuration")]
    [SerializeField] private bool showText = false;

    public override void Activate()
    {
        actuatorSpriteRenderer.sprite = deactivatedSprite;
    }

    public override void Deactivate()
    {
        actuatorSpriteRenderer.sprite = activatedSprite;
    }

    public void Update()
    {
        if(showText)
        {
            if(Keyboard.current.fKey.wasPressedThisFrame)
            {
                Activate();
            }
        }
    }

    public override void Reset()
    {
        Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        showText = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        showText = false;
        Deactivate();
    }
}
