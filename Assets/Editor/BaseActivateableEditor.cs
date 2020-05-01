using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(DoorActivateable))]
public class BaseActivateableEditor : Editor
{
    SerializedProperty activationMode;
    SerializedProperty isGated;
    SerializedProperty gates;
    SerializedProperty doorObject;
    SerializedProperty actuators;
    SerializedProperty resetActuator;
    SerializedProperty doorSpriteRenderer;
    SerializedProperty doorAnimator;
    SerializedProperty activationSequence;
    SerializedProperty sequenceResetTime;
    SerializedProperty boxColliderToDisable;

    bool gateSettings;

    private void OnEnable()
    {
        activationMode = serializedObject.FindProperty("activationMode");
        isGated = serializedObject.FindProperty("isGated");
        gates = serializedObject.FindProperty("gates");
        doorObject = serializedObject.FindProperty("doorObject");
        actuators = serializedObject.FindProperty("actuators");
        resetActuator = serializedObject.FindProperty("resetActuator");
        doorSpriteRenderer = serializedObject.FindProperty("doorSpriteRenderer");
        activationSequence = serializedObject.FindProperty("activationSequence");
        sequenceResetTime = serializedObject.FindProperty("sequenceResetTime");
        doorAnimator = serializedObject.FindProperty("anim");
        boxColliderToDisable = serializedObject.FindProperty("colliderToDisable");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(activationMode, new GUIContent("Activation Mode"));

        if (activationMode.enumValueIndex == 1)
        {
            DrawSequenceInspector();
        }
        else
        {
            DrawSingleInspector();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawSequenceInspector()
    {
        EditorGUILayout.LabelField(new GUIContent("Door Configuration"));
        EditorGUILayout.PropertyField(doorObject, new GUIContent("Door Object"));
        EditorGUILayout.PropertyField(resetActuator, new GUIContent("Reset Actuator"));
        EditorGUILayout.PropertyField(doorSpriteRenderer, new GUIContent("Door Sprite Renderer"));
        EditorGUILayout.PropertyField(doorAnimator, new GUIContent("Door Animator"));
        EditorGUILayout.PropertyField(boxColliderToDisable, new GUIContent("Collider To Disable"));

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField(new GUIContent("Gate Configuration"));
        DrawGateConfiguration();

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField(new GUIContent("Sequence Configuration"));
        EditorGUILayout.PropertyField(activationSequence, new GUIContent("Player Sequence"));
        EditorGUILayout.PropertyField(sequenceResetTime, new GUIContent("Sequence Reset Time"));
    }

    private void DrawSingleInspector()
    {
        EditorGUILayout.LabelField(new GUIContent("Door Configuration"));
        EditorGUILayout.PropertyField(doorObject, new GUIContent("Door Object"));
        EditorGUILayout.PropertyField(resetActuator, new GUIContent("Reset Actuator"));
        EditorGUILayout.PropertyField(doorSpriteRenderer, new GUIContent("Door Sprite Renderer"));

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField(new GUIContent("Gate Configuration"));
        DrawGateConfiguration();
    }

    private void DrawGateConfiguration()
    {
        isGated.boolValue = EditorGUILayout.BeginToggleGroup("Is Gated?", isGated.boolValue);
        EditorGUILayout.PropertyField(gates, new GUIContent("Gates"));
        EditorGUILayout.EndToggleGroup();
    }    
}
