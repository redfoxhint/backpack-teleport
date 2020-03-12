using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestGoal
{
    public Quest Quest;
    public string GoalDescription;
    public bool Completed;
    public int CurrentAmount;
    public int RequiredAmount;

    public virtual void Init()
    {

    }

    public void CheckCompleted()
    {
        GameEvents.onQuestUpdated.Invoke(Quest);

        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        Completed = true;
        Quest.CheckGoals();
        Debug.Log("Quest Complete");
    }
}
