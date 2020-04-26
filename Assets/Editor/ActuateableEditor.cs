using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Actuataeable))]
public class ActuatableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Actuataeable actuataeable = (Actuataeable)target;

        if (GUILayout.Button("Populate Sequence"))
        {
            actuataeable.PopulateSequence();
        }
    }
}
