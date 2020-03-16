using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestTrackerItem : MonoBehaviour
{
    [SerializeField] private Button itemButton;
    [SerializeField] private TextMeshProUGUI itemText;

    private QuestTrackerUI questTrackerUI;
    private Quest associatedQuest;

    public void Intialize(QuestTrackerUI _questManagerUI, Quest quest)
    {
        SetItemTitle(quest.QuestName);
        associatedQuest = quest;
        itemButton.onClick.AddListener(() => _questManagerUI.SetDescriptionPanel(quest));
        GameEvents.onQuestCompleted.AddListener(SetCompleted);
    }

    private void SetItemTitle(string newTitle)
    {
        itemText.SetText(newTitle);
    }

    private void SetCompleted(Quest quest)
    {
        itemText.fontStyle = FontStyles.Strikethrough; 
    }

}
