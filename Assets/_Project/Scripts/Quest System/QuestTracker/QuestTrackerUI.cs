using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestTrackerUI : MonoBehaviour
{
    [Header("Tracker UI Elements")]
    [SerializeField] private GameObject questTrackerItem;
    [SerializeField] private Transform trackerItemParent;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI currentAmountText;
    [SerializeField] private TextMeshProUGUI requiredAmountText;
    [SerializeField] private TextMeshProUGUI progressBarText;
    [SerializeField] private Image progressBarImage;

    private Quest associatedQuest;
    private bool firstOpen = true;

    private void Start()
    {
        ClearDescriptionPanel();
    }

    public void AddQuest(Quest quest, List<QuestTrackerItem> trackerItemList)
    {
        QuestTrackerItem item = Instantiate(questTrackerItem, trackerItemParent).GetComponent<QuestTrackerItem>();
        trackerItemList.Add(item);
        item.Intialize(this, quest);
    }

    public void SetDescriptionPanel(Quest quest)
    {
        int currentValue = quest.Goals[0].CurrentAmount;
        int requiredValue = quest.Goals[0].RequiredAmount;

        descriptionText.SetText(quest.QuestDescription);
        titleText.SetText(quest.QuestName);
        currentAmountText.SetText($"Current Amount: {currentValue}");
        requiredAmountText.SetText($"Required Amount: {requiredValue}");

        CalculateProgressValue(currentValue, requiredValue);
    }

    //public void UpdateQuest()
    //{
    //    SetDescriptionPanel(associatedQuest);
    //}

    private void CalculateProgressValue(int currentValue, int requiredValue)
    {
        int progressPercentage = (int)((double)currentValue / requiredValue * 100);
        float barValue = Mathf.Lerp(0f, 1f, (float)progressPercentage / 100f);
        progressBarImage.fillAmount = barValue;
        progressBarText.SetText($"{progressPercentage}%");

        Debug.Log(barValue);
    }

    private void ClearDescriptionPanel()
    {
        descriptionText.SetText("");
        titleText.SetText("");
        currentAmountText.SetText("");
        requiredAmountText.SetText("");
        progressBarImage.fillAmount = 0;
    }
}
