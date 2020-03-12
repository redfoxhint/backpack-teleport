using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class KillGoal : QuestGoal
{
    public EntityType EntityType;

    public KillGoal(Quest quest, string goalDescription, bool completed, int currentAmount, int requiredAmount, EntityType enemyType)
    {
        Quest = quest;
        GoalDescription = goalDescription;
        Completed = completed;
        CurrentAmount = currentAmount;
        RequiredAmount = requiredAmount;
        EntityType = enemyType;
    }

    public override void Init()
    {
        base.Init();
        GameEvents.onEntityKilled.AddListener(OnEnemyKilled);
    }

    public void OnEnemyKilled(EntityType entityType)
    {
        if(entityType == EntityType)
        {
            CurrentAmount++;
            CheckCompleted();
        }
    }
}
