using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Level", menuName = "Menu Options/New Level")]
public class LevelData : ScriptableObject
{
    public int levelBuildIndex;
    public string levelName;
    [TextArea] public string levelDescription;
    public Sprite levelPreview;
    public bool levelLocked;
}
