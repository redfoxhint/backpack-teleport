using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Menu Options/New Level")]
public class LevelData : ScriptableObject
{
    public int levelBuildIndex;
    public string levelName;
    public string levelSlug; // Ex: level_demo_one
    [TextArea] public string levelDescription;
    public Sprite levelPreview;
    public bool levelLocked;
    public AudioFiles defaultSoundtrack;
}
