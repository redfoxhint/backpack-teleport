using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpiderKillQuest : Quest
{
    public override void Init()
    {
        base.Init();

        QuestName = "Spider Kill Quest";
        QuestDescription = "Get rid of those pesky, long-legged spiders!";

        Goals = new List<QuestGoal>
        {
            new KillGoal(this, "Kill 3 Spiders", false, 0, 3, EntityType.ENEMY_SPIDER)
        };

        Goals.ForEach(g => g.Init());
    }
}
