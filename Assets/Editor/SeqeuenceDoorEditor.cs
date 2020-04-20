using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DoorActivateable))]
public class SeqeuenceDoorEditor : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    base.OnInspectorGUI();

    //    var door = target as DoorActivateable;
    //    SerializedObject serializedObject = new UnityEditor.SerializedObject(door);
    //    GUIStyle style = GetStyle();

    //    if (door.doorMode == DoorMode.SEQUENCE)
    //    {
    //        EditorGUILayout.Space(15f);
    //        EditorGUILayout.LabelField(new GUIContent("Sequence Configuration"), style);
    //    }
    //}

    //private GUIStyle GetStyle()
    //{
    //    GUIStyle myStyle = new GUIStyle();
    //    myStyle.fontSize = 15;
    //    myStyle.fontStyle = FontStyle.Bold;
    //    myStyle.normal.textColor = Color.yellow;

    //    return myStyle;
    //}

}
