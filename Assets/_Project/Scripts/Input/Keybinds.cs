using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Keybinds", menuName = "Keybinds/New Keybinds")]
public class Keybinds : ScriptableObject
{
    public KeyCode interact, pause;

    public KeyCode CheckKey(string key)
    {
        switch(key)
        {
            case "interact":
                return interact;

            case "pause":
                return pause;

            default:
                return KeyCode.None;
        }
    }
}
