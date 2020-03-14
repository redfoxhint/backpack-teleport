using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestTracker : MonoBehaviour
{
    [SerializeField] private GameObject trackerPanel;
    [SerializeField] private QuestTrackerUI trackerUI;

    private List<QuestTrackerItem> questsInTracker;

    private void Awake()
    {
        InputManager.Instance.InputActions.Player.ToggleQuestTracker.started += ToggleTracker;
    }

    private void Start()
    {
        questsInTracker = new List<QuestTrackerItem>();

        GameEvents.onQuestAssigned.AddListener(AddQuest);
        //GameEvents.onQuestUpdated.AddListener(UpdateQuest);
    }

    public void AddQuest(Quest quest)
    {
        trackerUI.AddQuest(quest, questsInTracker);
    }

    //public void UpdateQuest(Quest quest)
    //{
    //    foreach(QuestTrackerItem _quest in questsInTracker)
    //    {
    //        trackerUI.UpdateQuest();
    //    }
    //}

    public void ToggleTracker(InputAction.CallbackContext value)
    {
        bool state = trackerPanel.activeSelf;
        trackerPanel.SetActive(!state);

        if(trackerPanel.activeSelf)
        {
            GameManager.Instance.PauseGame();
            Cursor.visible = true;
        }
        else
        {
            GameManager.Instance.ResumeGame();
            Cursor.visible = false;
        }
    }
}
