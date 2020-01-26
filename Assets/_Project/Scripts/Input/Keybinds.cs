using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybind
{
    public string keybindName;
    public string keybindSlug;
    public KeyCode associatedKey;

    public Keybind(string keyName, string keySlug, KeyCode key)
    {
        keybindName = keyName;
        keybindSlug = keySlug;
        associatedKey = key;
    }
}


[CreateAssetMenu(fileName = "New Keybinds", menuName = "Keybinds/New Keybinds")]
public class Keybinds : ScriptableObject
{
    public KeyCode interact, pause, confirm, toggleConsole;

    public Dictionary<string, Keybind> keys = new Dictionary<string, Keybind>();

    public void InitializeKeys()
    {
        keys.Add("interact", new Keybind("Interact", "interact", interact));
        keys.Add("pause", new Keybind("Pause", "pause", pause));
        keys.Add("confirm", new Keybind("Confirm", "confirm", confirm));
        keys.Add("toggleConsole", new Keybind("ToggleConsole", "toggleConsole", toggleConsole));
    }

    public KeyCode CheckKey(string keyName)
    {
        foreach(KeyValuePair<string, Keybind> key in keys)
        {
            if(keyName == key.Key)
            {
                return key.Value.associatedKey;
            }
        }

        return KeyCode.None;

        //switch(key)
        //{
        //    case "interact":
        //        return interact;

        //    case "pause":
        //        return pause;

        //    case "confirm":
        //        return confirm;

        //    case "toggleConsole":
        //        return toggleConsole;

        //    default:
        //        return KeyCode.None;
        //}
    }

    public void UpdateKey(Keybind key)
    {
        switch (key.keybindSlug)
        {
            case "interact":
                interact = key.associatedKey;
                break;

            case "pause":
                pause = key.associatedKey;
                break;

            case "confirm":
                confirm = key.associatedKey;
                break;

            case "toggleConsole":
                toggleConsole = key.associatedKey;
                break;
        }
    }
}
