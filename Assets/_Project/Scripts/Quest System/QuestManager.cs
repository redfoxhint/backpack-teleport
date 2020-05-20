using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private bool dontDuplicateQuests = true;
    [SerializeField] private Quest testQuestToAdd;

    public List<Quest> AssignedQuests;
    
    private void Start()
    {
        testQuestToAdd = new SpiderKillQuest();
        AssignedQuests = new List<Quest>();
    }

    private void Update()
    {
        if(Keyboard.current.pKey.wasPressedThisFrame)
        {
            AssignQuest(testQuestToAdd);
        }
    }

    public void AssignQuest(Quest newQuest)
    {
        if(dontDuplicateQuests)
            foreach(Quest quest in AssignedQuests)
            {
                if (quest == newQuest) 
                    return;
            }

        AssignedQuests.Add(newQuest);
        newQuest.Init();

        GameEvents.onQuestAssigned.Invoke(newQuest);

        Notification onQuestAssignedNotification = new Notification("New Quest Added To Quest Tracker!", NotificationType.TEXT);
        GameEvents.onNotificationCreated.Invoke(onQuestAssignedNotification);

        Debug.Log($"<color=blue>New Quest Assigned: {newQuest.QuestName}</color>");
    }
}
