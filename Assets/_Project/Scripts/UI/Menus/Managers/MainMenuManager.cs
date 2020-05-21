using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CameraFading;

public class MainMenuManager : BaseMenuManager
{
    // Inspector Fields
    [Header("Main Menu")]
    [SerializeField] private RectTransform levelSelectScreen;
    [SerializeField] private RectTransform optionsScreen;

    [Header("Buttons")]
    [SerializeField] private Button levelSelectionButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    // Private Variables
    private LevelSelectScreen levelMenuManager;

    protected override void Awake()
    {
        base.Awake();
        levelMenuManager = GetComponent<LevelSelectScreen>();
    }

    private void Start()
    {
        AudioManager.Instance.FadeIn(AudioType.MUSIC, AudioFiles.ST_Calm1);
        Cursor.visible = true;
    }

    protected override void InitializeButtons()
    {
        base.InitializeButtons();
        RegisterButton(optionsButton, OnOptionsClicked);
        RegisterButton(levelSelectionButton, OnStoryButtonClicked);
        RegisterButton(exitButton, OnExitClicked);
    }

    private void OnOptionsClicked()
    {
        UpdateCurrentScreen(optionsScreen);
    }

    private void OnExitClicked()
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }
#endif
        Application.Quit();
    }

    private void OnStoryButtonClicked()
    {
        UpdateCurrentScreen(levelSelectScreen);
        levelMenuManager.SetDefaultLevel();
    }
}
