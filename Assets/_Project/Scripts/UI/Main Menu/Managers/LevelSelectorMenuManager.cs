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

    private LevelData currentLevelToLoad;

    [SerializeField] private CarouselMenu carouselMenu;

    private string defaultPlayButtonMessage = "Select A Level";

    private void Awake()
    {
        InitializeButtons();
        playButtonText.SetText(defaultPlayButtonMessage);

        if (carouselMenu != null)
        {
            carouselMenu.OnPageChangedEvent.AddListener(SelectLevel);
        }
    }

    private void InitializeButtons()
    {
        if (playLevelButton != null) playLevelButton.onClick.AddListener(OnPlayLevelButtonClicked);
        //if (newGameButton != null) newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        //if (loadGameButton != null) loadGameButton.onClick.AddListener(OnLoadGameButtonClicked);
        //if (continueButton != null) continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    private void SelectLevel()
    {
        Transform currentPageObject = carouselMenu.GetCurrentPageObject();
        if (currentPageObject == null) return;

        LevelData currentLevelData = currentPageObject.GetComponent<LevelSelector>().levelData;
        if (currentLevelData != null)
        {
            UpdateLevelPreview(currentLevelData);
        }
    }

    private void OnPlayLevelButtonClicked()
    {
        if (currentLevelToLoad.levelLocked) return; // TODO: Show message saying level is locked.

        FindObjectOfType<SceneLoader>().LoadLevelByIndex(currentLevelToLoad.levelBuildIndex);
    }

    private void UpdateLevelPreview(LevelData level)
    {
        currentLevelToLoad = level;
        playButtonText.SetText($"Play {level.levelName}");
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
        //UpdateLevelPreview(selected);
    }
}
