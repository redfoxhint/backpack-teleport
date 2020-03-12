using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class QuestNotification : MonoBehaviour
{
    [Header("Notification Configuration")]
    [SerializeField] private RectTransform titleBox;
    [SerializeField] private RectTransform descriptionBox;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private float notificationAnimateTime = 1f;
    [SerializeField] private float notificationAnimationNextPhaseTime = 0.5f;
    [SerializeField] private float notificationDeactivateDelay = 1f;

    private float originalTitleBoxHeight = 69;
    private float originalDescriptionBoxHeight = 309f;
    private bool isAnimating = false;

    // Components
    // private QuestData

    private void Awake()
    {
        TurnOffNotificationBox();
        GameEvents.onQuestAssigned.AddListener(OnQuestAssigned);
    }

    private void Update()
    {
        if(Keyboard.current.numpadPlusKey.wasPressedThisFrame && !isAnimating)
        {
            ActivateNotificationSequence();
        }

        if (Keyboard.current.numpadMinusKey.wasPressedThisFrame && !isAnimating)
        {
            DeactivateNotificationSequence();
        }
    }

    private void OnQuestAssigned(Quest assignedQuest)
    {
        SetQuest(assignedQuest.QuestName, assignedQuest.QuestDescription);
        ActivateNotificationSequence();
    }

    public void ActivateNotificationSequence()
    {
        isAnimating = true;

        Sequence notifSequence = DOTween.Sequence();
        notifSequence.AppendCallback(() => { ActivateNotificationBackground(titleBox, originalTitleBoxHeight); });
        notifSequence.AppendInterval(notificationAnimateTime + 0.1f);
        notifSequence.AppendCallback(() => ActivateText(titleText));

        notifSequence.AppendInterval(notificationAnimationNextPhaseTime);

        notifSequence.AppendCallback(() => { ActivateNotificationBackground(descriptionBox, originalDescriptionBoxHeight); });
        notifSequence.AppendInterval(notificationAnimateTime + 0.1f);
        notifSequence.AppendCallback(() => ActivateText(descriptionText));
        notifSequence.AppendCallback(() => { isAnimating = false; });

        notifSequence.AppendInterval(notificationDeactivateDelay);
        notifSequence.AppendCallback(() => { DeactivateNotificationSequence(); });
    }

    private void DeactivateNotificationSequence()
    {
        isAnimating = true;

        Sequence notifSequence = DOTween.Sequence();

        notifSequence.AppendCallback(() => DeactivateText(titleText));
        notifSequence.AppendInterval(notificationAnimateTime + 0.1f);
        notifSequence.AppendCallback(() => { DeactivateNotificationBackground(titleBox, originalTitleBoxHeight); });

        notifSequence.AppendInterval(notificationAnimationNextPhaseTime);

        notifSequence.AppendCallback(() => DeactivateText(descriptionText));
        notifSequence.AppendInterval(notificationAnimateTime + 0.1f);
        notifSequence.AppendCallback(() => { DeactivateNotificationBackground(descriptionBox, originalDescriptionBoxHeight); });
        notifSequence.AppendCallback(() => { isAnimating = false; });
    }

    private void SetQuest(string questTitle, string questDescription)
    {
        SetNotificationTitle(questTitle);
        SetNotificationDescription(questDescription);
    }

    private void ActivateNotificationBackground( RectTransform boxToAnimate, float maxSize)
    {
        DOVirtual.Float(0f, maxSize, notificationAnimateTime,
            (newHeight) =>
            {
                boxToAnimate.sizeDelta = new Vector2(boxToAnimate.sizeDelta.x, newHeight);
            });
    }

    private void DeactivateNotificationBackground(RectTransform boxToAnimate, float maxSize)
    {
        DOVirtual.Float(maxSize, 0f, notificationAnimateTime,
            (newHeight) =>
            {
                boxToAnimate.sizeDelta = new Vector2(boxToAnimate.sizeDelta.x, newHeight);
            });
    }

    private void ActivateText(TextMeshProUGUI textToAnimate)
    {
        textToAnimate.DOColor(Color.white, notificationAnimateTime);
    }

    private void DeactivateText(TextMeshProUGUI textToAnimate)
    {
        textToAnimate.DOColor(Color.clear, notificationAnimateTime);
    }

    private void SetNotificationTitle(string newText)
    {
        if (titleText == null) return;
        titleText.SetText(newText);
    }

    private void SetNotificationDescription(string newText)
    {
        if (descriptionText == null) return;
        descriptionText.SetText(newText);
    }

    private void TurnOffNotificationBox()
    {
        titleBox.sizeDelta = new Vector2(titleBox.sizeDelta.x, 0f);
        descriptionBox.sizeDelta = new Vector2(descriptionBox.sizeDelta.x, 0f);

        titleText.color = Color.clear;
        descriptionText.color = Color.clear;
    }
}
