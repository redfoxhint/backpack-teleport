using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Menu Options/New Level")]
public class LevelData : ScriptableObject
{
    public int levelBuildIndex;
    public string levelName;
    [TextArea] public string levelDescription;
    public Sprite levelPreview;
}
