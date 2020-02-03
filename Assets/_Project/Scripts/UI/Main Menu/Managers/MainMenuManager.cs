using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CameraFading;

public class MainMenuManager : MonoBehaviour
{
    // Inspector Fields
    [Header("Main Menu")]
    [SerializeField] private RectTransform mainMenu;
    [SerializeField] private RectTransform levelSelectionMenu;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button levelSelectionButton;

    [Header("Options Menu")]
    [SerializeField] private RectTransform optionsMenu;

    // Private Variables
    private RectTransform currentMenu;
    private OptionsMenuManager optionsMenuManager;
    private LevelSelectorMenuManager levelMenuManager;

    private void Awake()
    {
        InitializeButtons();
        levelMenuManager = GetComponent<LevelSelectorMenuManager>();
    }

    private void InitializeButtons()
    {
        if (optionsButton != null) optionsButton.onClick.AddListener(OnOptionsClicked);
        if (exitButton != null) exitButton.onClick.AddListener(OnExitClicked);
        if (backButton != null) backButton.onClick.AddListener(OnBackButtonClicked);
        if (levelSelectionButton != null) levelSelectionButton.onClick.AddListener(OnStoryButtonClicked);
    }

    private void OnOptionsClicked()
    {
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
        currentMenu = optionsMenu;
        EnableBackButton();
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

    private void OnBackButtonClicked()
    {
        currentMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        DisableBackButton();
    }

    private void OnStoryButtonClicked()
    {
        mainMenu.gameObject.SetActive(false);
        levelSelectionMenu.gameObject.SetActive(true);
        currentMenu = levelSelectionMenu;
        EnableBackButton();
        levelMenuManager.SetDefaultLevel();
    }

    private void EnableBackButton()
    {
        backButton.gameObject.SetActive(true);
    }

    private void DisableBackButton()
    {
        backButton.gameObject.SetActive(false);
    }
}
