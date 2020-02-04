using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : BaseMenu
{
    [Header("Pause Menu")]
    [SerializeField] private RectTransform saveScreen;
    [SerializeField] private RectTransform loadScreen;
    [SerializeField] private RectTransform optionsScreen;
    [SerializeField] private RectTransform testScreen;
    [SerializeField] private RectTransform testScreen2;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button testButton;
    [SerializeField] private Button testButton2;

    // Private Variables

    protected override void InitializeButtons()
    {
        base.InitializeButtons();
        RegisterButton(optionsButton, OnOptionsButtonClicked);
        RegisterButton(saveButton, OnSaveButtonClicked);
        RegisterButton(loadButton, OnLoadButtonClicked);
        RegisterButton(resumeButton, OnResumeButtonClicked);
        RegisterButton(mainMenuButton, OnMainMenuButtonClicked);
        RegisterButton(testButton, OnTestButtonClicked);
        RegisterButton(testButton2, OnTestButton2Clicked);
    }

    private void OnTestButton2Clicked()
    {
        UpdateCurrentScreen(testScreen2);
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

    private void OnTestButtonClicked()
    {
        UpdateCurrentScreen(testScreen);
    }
}
