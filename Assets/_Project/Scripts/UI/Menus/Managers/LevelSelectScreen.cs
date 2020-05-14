using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using Malee;

[System.Serializable]
public class LevelSelectDataList : ReorderableArray<LevelData> { }

public class LevelSelectScreen : MonoBehaviour
{
    [Header("Level Screen")]
    // Add additional screens here
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject levelSlotPrefab;

    [Header("Buttons")]
    [SerializeField] private Button playLevelButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button continueButton;

    [Header("Other Level Selection UI")]
    [SerializeField] private TextMeshProUGUI playButtonText;

    [Header("Components")]
    [SerializeField] private CarouselMenu carouselMenu;

    [Reorderable(elementNameProperty = "levelName")] 
    public LevelSelectDataList levelSelectorDatas;

    // Private Variables
    private LevelData currentLevelToLoad;
    private LevelSelector lastSelectedLevel;
    private string defaultPlayButtonMessage = "Select A Level";

    private void Awake()
    {
        InitializeButtons();
        playButtonText.SetText(defaultPlayButtonMessage);

        if (carouselMenu != null)
        {
            carouselMenu.OnPageChangedEvent.AddListener(SelectLevel);
        }

        InitLevels();
    }

    private void InitializeButtons()
    {
        if (playLevelButton != null) playLevelButton.onClick.AddListener(OnPlayLevelButtonClicked);
        //RegisterButton(playLevelButton, OnPlayLevelButtonClicked);
        //RegisterButton(continueButton, OnContinueButtonClicked);
        //RegisterButton(newGameButton, OnNewGameButtonClicked);
        //RegisterButton(loadGameButton, OnLoadGameButtonClicked);
    }

    private void SelectLevel()
    {
        Transform currentPageObject = carouselMenu.GetCurrentPageObject();
        if (currentPageObject == null) return;

        LevelSelector selectedLevel = currentPageObject.GetComponent<LevelSelector>();
        selectedLevel.Select();

        if(lastSelectedLevel != null)
        {
            lastSelectedLevel.Deselect();
        }

        lastSelectedLevel = selectedLevel;

        LevelData currentLevelData = selectedLevel.levelData;
        if (currentLevelData != null)
        {
            UpdateLevelPreview(currentLevelData);
        }
    }

    private void InitLevels()
    {
        foreach (LevelData x in levelSelectorDatas)
        {
            LevelSelector levelSelector = Instantiate(levelSlotPrefab, slotParent).GetComponent<LevelSelector>();
            levelSelector.levelData = x;
            LogUtils.LogWarning($"{levelSelector.levelData.levelName} was added.");
        }
    }

    public void SetDefaultLevel()
    {
        Transform defaultPage = carouselMenu.GetDefaultPage();

        LevelSelector selectedLevel = defaultPage.GetComponent<LevelSelector>();
        selectedLevel.Select();
        lastSelectedLevel = selectedLevel;

        LevelData defaultLevelData = selectedLevel.GetComponent<LevelSelector>().levelData;

        if (defaultLevelData != null) UpdateLevelPreview(defaultLevelData);
    }

    private void OnPlayLevelButtonClicked()
    {
        if (currentLevelToLoad == null) return;
        if (IfLevelLocked()) return;

        SceneLoader.Instance.LoadLevel(currentLevelToLoad.levelName, currentLevelToLoad.defaultSoundtrack);
    }

    private bool IfLevelLocked()
    {
        if (currentLevelToLoad.levelLocked)
        {
            Debug.Log($"Level selected is <color=red>locked!</color>");
            return true;
        }

        return false;
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
}
