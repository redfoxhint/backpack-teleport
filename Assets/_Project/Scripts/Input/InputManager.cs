using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : PersistentSingleton<InputManager>
{
    public InputActions InputActions { get; private set; }

    public Vector2 MovementInput { get; private set; }
    public Vector2 JoystickInput { get; private set; }
    public bool UseGamepad { get; set; }

    public Action<InputDevice, InputDeviceChange> OnInputDeviceChangeDetected;

    private void OnEnable()
    {
        InitalizeInput();
    }

    private void OnDisable()
    {
        // Clean up subcribed inputs
        InputActions.Player.Movement.canceled -= OnMovement;
        InputActions.Player.CursorControl.canceled -= OnCursor;

        InputActions.Disable();
    }

    private void InitalizeInput()
    {
        InputActions = new InputActions();

        // Subscribe to input actions
        InputActions.Player.Movement.performed += OnMovement;
        InputActions.Player.CursorControl.performed += OnCursor;

        InputActions.Enable();
        UseGamepad = true;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        MovementInput = value.ReadValue<Vector2>();
    }

    public void OnCursor(InputAction.CallbackContext value)
    {
        JoystickInput = value.ReadValue<Vector2>();
    }

    public bool IsGamepadConnected()
    {
        var connectedGamepad = Gamepad.current;

        if(connectedGamepad != null)
        {
            return true;
        }

        return false;
    }
}
