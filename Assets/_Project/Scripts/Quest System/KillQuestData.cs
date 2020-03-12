using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kill Quest", menuName = "Quests/New Kill Quest")]
public class KillQuestData : BaseQuestData
{
    public int killsNeeded { get; }
}
