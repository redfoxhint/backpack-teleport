using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraZoomTrigger))]
public class CameraZoomTriggerEditor : Editor
{
    SerializedProperty zoomAmount;
    SerializedProperty zoomSpeed;

    bool useCustomSettings = false;

    private void OnEnable()
    {
        zoomAmount = serializedObject.FindProperty("zoomAmount");
        zoomSpeed = serializedObject.FindProperty("zoomSpeed");
    }

    public override void OnInspectorGUI()
    {
        useCustomSettings = EditorGUILayout.BeginToggleGroup(new GUIContent("Use Custom Settings"), useCustomSettings);
        EditorGUILayout.PropertyField(zoomAmount);
        EditorGUILayout.PropertyField(zoomSpeed);
        EditorGUILayout.EndToggleGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
