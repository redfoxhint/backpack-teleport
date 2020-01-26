using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionsMenuManager : MonoBehaviour
{
    [Header("Option Tabs")]
    [SerializeField] private RectTransform gameplayTab;
    [SerializeField] private RectTransform graphicsTab;
    [SerializeField] private RectTransform controlsTab;
    [SerializeField] private RectTransform soundTab;

    [Space]

    [Header("Other Buttons")]
    [SerializeField] private Button soundButton;
    [SerializeField] private Button saveButton;

    [Space]

    [Header("Tab Buttons")]
    [SerializeField] private Button gameplayButton;
    [SerializeField] private Button graphicsButton;
    [SerializeField] private Button controlsButton;

    [Space]

    [Header("Gameplay Options")]
    [SerializeField] private Toggle doGodModeToggle;  // FOR TESTING
    [SerializeField] private Toggle doSuperSpeedToggle; // FOR TESTING
    [SerializeField] private Toggle doNoStaminaToggle; // FOR TESTING

    [Header("Sound Options")]
    [SerializeField] private Slider masterVolumeSlider;


    private RectTransform currentTabOpen = null;

    private void Awake()
    {
        InitializeOptions();
        currentTabOpen = gameplayTab;
        LoadOptions();
    }

    private void InitializeOptions()
    {
        // Tabs
        if (gameplayButton != null) gameplayButton.onClick.AddListener(delegate { SetCurrentTab(gameplayTab); });
        if (graphicsButton != null) graphicsButton.onClick.AddListener(delegate { SetCurrentTab(graphicsTab); });
        if (controlsButton != null) controlsButton.onClick.AddListener(delegate { SetCurrentTab(controlsTab); });

        // Other Buttons
        if (soundButton != null) soundButton.onClick.AddListener(delegate { SetCurrentTab(soundTab); });
        if (saveButton != null) saveButton.onClick.AddListener(OnSaveClicked);

        // Gameplay Options
        if (doGodModeToggle != null) doGodModeToggle.onValueChanged.AddListener(delegate { SetGodMode(doGodModeToggle.isOn); });
        if (doSuperSpeedToggle != null) doSuperSpeedToggle.onValueChanged.AddListener(delegate { SetSuperSpeed(doSuperSpeedToggle.isOn); });
        if (doNoStaminaToggle != null) doNoStaminaToggle.onValueChanged.AddListener(delegate { SetNoStamina(doNoStaminaToggle.isOn); });

        // Graphics Options

        // Control Options

        // Sound Options
        if (masterVolumeSlider != null) masterVolumeSlider.onValueChanged.AddListener(delegate { SetMasterVolume(masterVolumeSlider.value); });
    }

    private void SaveOptions()
    {
        OptionsData options = new OptionsData
            (
                masterVolumeSlider.value
            );

        FileReadWrite.WriteToJsonFile(options);
    }

    private void LoadOptions()
    {
        OptionsData options = FileReadWrite.ReadFromJsonFile<OptionsData>();
        
        if(options != null)
        {
            masterVolumeSlider.value = options.masterVolumeValue;
        }
    }

    private void SetCurrentTab(RectTransform tab)
    {
        if (currentTabOpen != null)
        {
            currentTabOpen.gameObject.SetActive(false);
        }

        currentTabOpen = tab;

        tab.gameObject.SetActive(true);
    }

    private void OnSaveClicked()
    {
        Debug.Log("Options saved.");
        SaveOptions();
    }

    #region Gameplay Region
    private void SetGodMode(bool value)
    {
        Debug.Log($"God mode now {value}.");
    }

    private void SetSuperSpeed(bool value)
    {
        Debug.Log($"Super Speed now {value}.");
    }

    private void SetNoStamina(bool value)
    {
        Debug.Log($"No Stamina now {value}.");
    }

    #endregion

    #region Graphics Region

    #endregion

    #region Controls Region

    #endregion

    #region Sound Region
    private void SetMasterVolume(float value)
    {
        Debug.Log($"Master Volume: {value}");
    }

    #endregion
}
