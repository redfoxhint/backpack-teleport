using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(DoorActivateable))]
public class BaseActivateableEditor : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();
    //    DoorActivateable activateable = (DoorActivateable)target;

    //    if (GUILayout.Button("Configure Activateable"))
    //    {
    //        List<BaseActivateable> activateables = activateable.Configure();
    //        LogUtils.Log(activateables.Count.ToString());
    //    }
    //}
}
