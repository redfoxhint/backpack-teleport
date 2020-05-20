﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New World Space Tutorial", menuName = "Tutorial/New World Space Tutorial")]
public class WorldSpaceTutorialData : ScriptableObject
{
    public bool hasMultipleButtons;
    public List<Sprite> buttonPressSprites;
    [TextArea]public string tutorialText;
}