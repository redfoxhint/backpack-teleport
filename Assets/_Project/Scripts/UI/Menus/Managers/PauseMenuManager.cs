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

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button mainMenuButton;

    // Private Variables

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
        Debug.Log("Game resumed.");
    }
    private void OnMainMenuButtonClicked()
    {
        Debug.Log("Returned to main menu.");
    }
}
