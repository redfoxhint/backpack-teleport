using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuManager : BaseMenuManager
{
    [Header("Pause Menu")]
    [SerializeField] private RectTransform optionsScreen;
    [SerializeField] private RectTransform menu;
    [SerializeField] private AudioMixerSnapshot pausedSnapshot;
    [SerializeField] private AudioMixerSnapshot unpausedSnapshot;


    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;

    // Private Variables

    private void Update()
    {
        if(InputManager.Instance.InputActions.Player.TogglePauseMenu.triggered)
        {
            if(!GameManager.Instance.GamePaused && !NotificationManager.Instance.NotificationUI.FullscreenPanelOpen)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    protected override void InitializeButtons()
    {
        base.InitializeButtons();
        RegisterButton(optionsButton, OnOptionsButtonClicked);
        RegisterButton(resumeButton, OnResumeButtonClicked);
        RegisterButton(mainMenuButton, OnMainMenuButtonClicked);
    }

    private void OnOptionsButtonClicked()
    {
        UpdateCurrentScreen(optionsScreen);
    }

    private void OnResumeButtonClicked()
    {
        Unpause();
    }
    private void OnMainMenuButtonClicked()
    {
        SceneLoader.Instance.LoadLevel(1);
        Unpause();
        Cursor.visible = true;
    }

    private void Pause()
    {
        GameManager.Instance.PauseGame();
        menu.gameObject.SetActive(true);
        pausedSnapshot.TransitionTo(0.01f);
    }

    private void Unpause()
    {
        GameManager.Instance.ResumeGame();
        menu.gameObject.SetActive(false);
        unpausedSnapshot.TransitionTo(0.01f);
    }
}
