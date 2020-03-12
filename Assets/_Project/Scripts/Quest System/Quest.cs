using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class Quest
{
    public List<QuestGoal> Goals;
    public string QuestName;
    public string QuestDescription;
    public bool Completed;

    public virtual void Init()
    {

    }

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
    }

    public void GiveReward()
    {
        // TODO: Implement inventory for this.
    }
}
