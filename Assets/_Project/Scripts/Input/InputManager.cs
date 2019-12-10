using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : PersistentSingleton<InputManager>
{
    public Keybinds keybinds;

    private void OnEnable()
    {
        keybinds = Resources.Load<ScriptableObject>("Keybinds") as Keybinds;
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
}
