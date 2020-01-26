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
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;

    [Header("Options Menu")]
    [SerializeField] private RectTransform optionsMenu;

    // Private Variables

    // Components
    private OptionsMenuManager optionsMenuManager;

    private void Awake()
    {
        InitializeButtons();
        optionsMenuManager = FindObjectOfType<OptionsMenuManager>();
    }

    private void InitializeButtons()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayClicked);
        }

        if (optionsButton != null)
        {
            optionsButton.onClick.AddListener(OnOptionsClicked);
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitClicked);
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }
    }

    private void OnPlayClicked()
    {

    }

    private void OnOptionsClicked()
    {
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
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
        optionsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
