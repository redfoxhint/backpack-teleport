using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    KILL_COUNT
}

public abstract class BaseQuestData : ScriptableObject
{
    public string title { get; }
    public string description { get; }
    public QuestType questType { get; }
}
