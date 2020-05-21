using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour
{
    private enum AnimationAxis { X, Y }

    [Header("Notification Configuration")]
    [SerializeField] private RectTransform notificationBox;
    [SerializeField] private RectTransform fullscreenNotificationPanel;
    [SerializeField] private TextMeshProUGUI notificationText;

    [Header("Fullscreen Notification Configuration")]
    [SerializeField] private Image fullscreenImage;
    [SerializeField] private TextMeshProUGUI fullscreenText;
    [SerializeField] private Button closeFullscreenPanelButton;

    [SerializeField] private float notificationBoxAnimationTime = 0.8f;
    [SerializeField] private float notificationLifeTime = 2f;
    [SerializeField] private AnimationAxis animationAxis;

    // Private Variables
    private const float xMaxSize = 384f;
    private const float yMaxSize = 92f;
    private bool closed;
    private bool fullscreenPanelOpen = false;

    public bool FullscreenPanelOpen { get => fullscreenPanelOpen; }

    // Components
    private NotificationManager notificationManager;

    // Events
    public System.Action OnAnimationFinished;

    private void Awake()
    {
        notificationManager = GetComponent<NotificationManager>();

        notificationText.color = new Color(notificationText.color.r, notificationText.color.g, notificationText.color.b, 0f);
        notificationBox.sizeDelta = new Vector2(0f, notificationBox.sizeDelta.y);
        closed = true;

        closeFullscreenPanelButton.onClick.AddListener(DeactivateFullscreenPanel);
    }

    public void ShowNotification(Notification notification)
    {
        switch(notification.NotificationType)
        {
            case NotificationType.NORMAL:
                ShowNormalNotification(notification);
                break;

            case NotificationType.FULLSCREEN:
                ShowFullscreenNotification(notification);
                break;
        }
    }

    private void ShowNormalNotification(Notification notification)
    {
        SetText(notification.NotificationText);

        Sequence s = DOTween.Sequence();

        if (closed)
        {
            closed = false;
            s.AppendCallback(() => ActivateNotificationBackground());
            s.AppendInterval(notificationBoxAnimationTime);
        }

        s.AppendCallback(() => { DOVirtual.Float(0, 1, 0.5f, SetAlpha); });
        s.AppendInterval(notificationLifeTime);
        s.AppendCallback(() => { DOVirtual.Float(1, 0, 0.5f, SetAlpha); });
        s.AppendInterval(1f);

        if (notificationManager.NotificationBuffer.Count == 1)
        {
            closed = true;
            s.AppendCallback(() => DeactivateNotificationBackground());
            s.AppendInterval(notificationBoxAnimationTime);
        }

        s.OnComplete(() => { OnAnimationFinished.Invoke(); });
    }

    private void ShowFullscreenNotification(Notification notification)
    {
        SetText($"New Tutorial - Press E to view.");
        StartCoroutine(FullscreenNotificationRoutine(notification));
    }

    private IEnumerator FullscreenNotificationRoutine(Notification notification)
    {
        ActivateNotificationBackground();
        yield return new WaitForSeconds(notificationBoxAnimationTime);
        DOVirtual.Float(0, 1, 0.5f, SetAlpha);

        while (!Keyboard.current.eKey.wasPressedThisFrame)
        {
            yield return null;
        }

        DOVirtual.Float(1, 0, 0.5f, SetAlpha);
        yield return new WaitForSeconds(1f);
        DeactivateNotificationBackground();

        ActivateFullscreenPanel(notification);
        
        while(!Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }

        DeactivateFullscreenPanel();

        yield return new WaitForSeconds(notificationBoxAnimationTime);
        OnAnimationFinished.Invoke();
    }

    private void SetText(string text)
    {
        notificationText.SetText(text);
    }

    private void SetAlpha(float alpha)
    {
        Color newColor = new Color(notificationText.color.r, notificationText.color.g, notificationText.color.b, alpha);
        notificationText.color = newColor;
    }

    private void ActivateNotificationBackground()
    {
        switch (animationAxis)
        {
            case AnimationAxis.X:
                DOVirtual.Float(0f, xMaxSize, notificationBoxAnimationTime,
            (newWidth) =>
            {
                notificationBox.sizeDelta = new Vector2(newWidth, yMaxSize);
            });
                break;
            case AnimationAxis.Y:
                DOVirtual.Float(0f, yMaxSize, notificationBoxAnimationTime,
            (newWidth) =>
            {
                notificationBox.sizeDelta = new Vector2(xMaxSize, newWidth);
            });
                break;
        }
    }

    private void DeactivateNotificationBackground()
    {
        switch (animationAxis)
        {
            case AnimationAxis.X:
                DOVirtual.Float(xMaxSize, 0, notificationBoxAnimationTime,
            (newWidth) =>
            {
                notificationBox.sizeDelta = new Vector2(newWidth, yMaxSize);
            }).SetUpdate(true);
                break;
            case AnimationAxis.Y:
                DOVirtual.Float(yMaxSize, 0f, notificationBoxAnimationTime,
            (newWidth) =>
            {
                notificationBox.sizeDelta = new Vector2(xMaxSize, newWidth);
            }).SetUpdate(true);
                break;
        }
    }

    private void ActivateFullscreenPanel(Notification notification)
    {
        GameManager.Instance.PauseGame();
        CameraFunctions.Instance.SetScreenColor(Color.grey);
        Cursor.visible = true;
        fullscreenPanelOpen = true;
        AudioManager.Instance.PlaySoundEffect(AudioFiles.SFX_Notification3);
        CameraFunctions.Instance.FadeDOFIn(() =>
        {
            fullscreenText.SetText(notification.NotificationText);
            fullscreenImage.sprite = notification.NotificationSprite;
            fullscreenNotificationPanel.gameObject.SetActive(true);
        });
    }

    private void DeactivateFullscreenPanel()
    {
        fullscreenNotificationPanel.gameObject.SetActive(false);
        CameraFunctions.Instance.SetScreenColor(Color.white);
        GameManager.Instance.ResumeGame();
        AudioManager.Instance.PlaySoundEffect(AudioFiles.SFX_Notification1);

        Cursor.visible = false;
        fullscreenPanelOpen = false;
        CameraFunctions.Instance.FadeDOFOut();
    }
}
