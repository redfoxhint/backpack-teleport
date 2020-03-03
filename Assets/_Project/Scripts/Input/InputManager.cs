using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : PersistentSingleton<InputManager>
{
    public Keybinds keybinds;

    public Vector2 wasdInput;
    public InputActions InputActions { get; private set; }

    private void OnEnable()
    {
        keybinds = Resources.Load<ScriptableObject>("Keybinds") as Keybinds;
        keybinds.InitializeKeys();

        InputActions = new InputActions();
        InputActions.Player.Movement.performed += OnMovement;
        InputActions.Player.Movement.canceled -= OnMovement;
        InputActions.Player.BasicAttack.performed += OnBasicAttack;
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
        InputActions.Player.Movement.canceled -= OnMovement;
        InputActions.Player.BasicAttack.performed -= OnBasicAttack;
    }

    public bool KeyDown(string key)
    {
        if (Input.GetKeyDown(keybinds.CheckKey(key)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        wasdInput = value.ReadValue<Vector2>();
    }

    public void OnBasicAttack(InputAction.CallbackContext value)
    {
        Debug.Log("Attacked");
    }
}
