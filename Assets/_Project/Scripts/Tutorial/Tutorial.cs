using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Tutorial", menuName = "Tutorial/New Tutorial")]
public class Tutorial : ScriptableObject
{
    public TutorialPage[] pages;
    public int AmountOfPages 
    {
        get
        {
            return pages.Length;
        }
    }
}

[System.Serializable]
public class TutorialPage
{
    public string title;
    [TextArea] public string description;
    public Sprite tutorialImage;
}

