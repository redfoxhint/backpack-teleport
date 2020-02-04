using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : BaseMenuManager
{
    [Header("Pause Menu")]
    [SerializeField] private RectTransform saveScreen;
    [SerializeField] private RectTransform loadScreen;
    [SerializeField] private RectTransform optionsScreen;
    [SerializeField] private RectTransform menu;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button mainMenuButton;

    // Private Variables

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!GameManager.Instance.GamePaused)
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
        RegisterButton(saveButton, OnSaveButtonClicked);
        RegisterButton(loadButton, OnLoadButtonClicked);
        RegisterButton(resumeButton, OnResumeButtonClicked);
        RegisterButton(mainMenuButton, OnMainMenuButtonClicked);
    }

    private void OnOptionsButtonClicked()
    {
        UpdateCurrentScreen(optionsScreen);
    }

    private void OnSaveButtonClicked()
    {
        UpdateCurrentScreen(saveScreen);
    }

    private void OnLoadButtonClicked()
    {
        UpdateCurrentScreen(loadScreen);
    }

    private void OnResumeButtonClicked()
    {
        Unpause();
    }
    private void OnMainMenuButtonClicked()
    {
        FindObjectOfType<SceneLoader>().LoadLevelByIndex(0);
        Unpause();
        Cursor.visible = true;
    }

    private void Pause()
    {
        GameManager.Instance.PauseGame();
        menu.gameObject.SetActive(true);
    }

    private void Unpause()
    {
        GameManager.Instance.ResumeGame();
        menu.gameObject.SetActive(false);
    }
}
