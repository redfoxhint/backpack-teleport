using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackTeleport.Events;
using BackpackTeleport.Dialogue;

[CreateAssetMenu(fileName = "New Tutorial Segement", menuName = "Tutorial/New Tutorial Segment")]
public class TutorialSegment : ScriptableObject
{
	public string tutorialName;
    public Dialogue tutorialDialogue;
	public VoidEvent eventToListenTo;
}
