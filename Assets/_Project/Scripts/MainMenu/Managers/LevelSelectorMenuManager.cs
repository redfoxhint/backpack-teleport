using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class LevelSelectorMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playLevelButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button continueButton;

    [Header("Level Selection UI")]
    [SerializeField] private Image levelPreviewImage;
    [SerializeField] private TextMeshProUGUI levelTitleText;
    [SerializeField] private TextMeshProUGUI levelDescriptionText;
    [SerializeField] private TextMeshProUGUI playButtonText;

    [SerializeField] private LevelSelectionBar selectorBar;

    private string defaultPlayButtonMessage = "Select A Level";

    private void Awake()
    {
        InitializeButtons();
        playButtonText.SetText(defaultPlayButtonMessage);
    }

    private void Start()
    {
        selectorBar.OnLevelSelected.AddListener(OnLevelSelected);
    }

    private void InitializeButtons()
    {
        if (playLevelButton != null) playLevelButton.onClick.AddListener(OnPlayLevelButtonClicked);
        if (newGameButton != null) newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        if (loadGameButton != null) loadGameButton.onClick.AddListener(OnLoadGameButtonClicked);
        if (continueButton != null) continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    private void OnPlayLevelButtonClicked()
    {
        LevelSelector currentlySelectedLevel = selectorBar.GetCurrentSelector();

        if (currentlySelectedLevel == null)
        {
            Debug.Log("No selector selected.");
            return;
        }

        FindObjectOfType<SceneLoader>().LoadLevelByIndex(currentlySelectedLevel.levelData.levelBuildIndex);
    }

    private void OnContinueButtonClicked()
    {
        Debug.Log("Continue clicked");
    }

    private void OnLoadGameButtonClicked()
    {
        Debug.Log("Load game clicked");
    }

    private void OnNewGameButtonClicked()
    {
        Debug.Log("New game clicked");
    }

    private void OnLevelSelected(LevelSelector selected)
    {
        UpdateLevelPreview(selected);
    }

    private void UpdateLevelPreview(LevelSelector selected)
    {
        
        if (selected == null) return;

        levelPreviewImage.sprite = selected.levelData.levelPreview;
        levelTitleText.SetText(selected.levelData.levelName);
        levelDescriptionText.SetText(selected.levelData.levelDescription);
        playButtonText.SetText(selected.levelData.levelName);
    }
}
