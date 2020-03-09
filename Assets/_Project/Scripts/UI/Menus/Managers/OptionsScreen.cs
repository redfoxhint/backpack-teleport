using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;

/*
 * TODO: AUTOMATICALLY GENERATE REBIND BUTTONS IN CONTROLS MENU
 */

public class OptionsScreen : MonoBehaviour
{
    [Header("Option Tabs")]
    [SerializeField] private RectTransform gameplayTab;
    [SerializeField] private RectTransform graphicsTab;
    [SerializeField] private RectTransform controlsTab;
    [SerializeField] private RectTransform soundTab;

    [Space]

    [Header("Menu Elements")]
    [SerializeField] private Button saveButton;

    [Space]

    [Header("Gameplay Options")]
    [SerializeField] private Toggle useGamepadToggle;

    [Header("Graphics Options")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vSyncToggle;

    //[Header("Control Options")]

    [Header("Sound Options")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    private int pageIndex = 0;

    private Resolution[] resolutions;

    private void Awake()
    {
        InitializeButtons();
        CreateResolutionOptions();
        LoadOptions();
    }

    private void Start()
    {
        //Debug.Log(inputActions.asset.actionMaps.Count);
        //CreateRebindButtons();
    }

    private void InitializeButtons()
    {
        // Menu Buttons
        if (saveButton != null) saveButton.onClick.AddListener(OnSaveClicked);

        // Gameplay Options
        if (useGamepadToggle != null) useGamepadToggle.onValueChanged.AddListener(delegate { SetUseGamepad(useGamepadToggle, useGamepadToggle.isOn); });

        // Graphics Options
        if (resolutionDropdown != null) resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
        if (fullscreenToggle != null) fullscreenToggle.onValueChanged.AddListener(delegate { SetFullscreen(fullscreenToggle.isOn); });
        if (vSyncToggle != null) vSyncToggle.onValueChanged.AddListener(delegate { ToggleVsync(vSyncToggle.isOn); });

        // Control Options

        // Sound Options
        if (masterVolumeSlider != null) masterVolumeSlider.onValueChanged.AddListener(delegate { SetMasterVolume(masterVolumeSlider.value); });
        if (soundVolumeSlider != null) soundVolumeSlider.onValueChanged.AddListener(delegate { SetSoundVolume(soundVolumeSlider.value); });
        if (musicVolumeSlider != null) musicVolumeSlider.onValueChanged.AddListener(delegate { SetMusicVolume(musicVolumeSlider.value); });
    }

    #region Saving & Loading
    private void SaveOptions()
    {
        OptionsData options = new OptionsData
            (
                useGamepadToggle.isOn,
                fullscreenToggle.isOn,
                vSyncToggle.isOn,
                masterVolumeSlider.value,
                soundVolumeSlider.value,
                musicVolumeSlider.value
            );

        FileReadWrite.WriteToJsonFile(options);
    }

    private void LoadOptions()
    {
        OptionsData options = FileReadWrite.ReadFromJsonFile<OptionsData>();

        if (options != null)
        {
            useGamepadToggle.isOn = options.useGamepad;
            fullscreenToggle.isOn = options.fullScreen;
            vSyncToggle.isOn = options.vSync;
            masterVolumeSlider.value = options.masterVolumeValue;
            soundVolumeSlider.value = options.soundVolumeValue;
            musicVolumeSlider.value = options.musicVolumeValue;
        }
    }

    private void OnSaveClicked()
    {
        Debug.Log("Options saved.");
        SaveOptions();
    }

    #endregion

    #region Gameplay Region
    private void SetUseGamepad(Toggle toggle, bool value)
    {
        bool gamepadConnected = InputManager.Instance.IsGamepadConnected();

        if(gamepadConnected)
        {
            InputManager.Instance.UseGamepad = value;
            Debug.Log("Now using gamepad.");
        }
        else
        {
            useGamepadToggle.isOn = false;
            InputManager.Instance.UseGamepad = false;
            Debug.Log("Gamepad not found, using keyboard and mouse input instead.");
        }
    }

    #endregion

    #region Graphics Region

    private void CreateResolutionOptions()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void SetFullscreen(bool isOn)
    {
        Screen.fullScreen = isOn;
    }

    private void ToggleVsync(bool isOn)
    {
        QualitySettings.vSyncCount = Convert.ToInt32(isOn);
    }

    private void SetTestSlider(float value)
    {
        //Debug.Log($"Test Slider: {value}");
    }

    #endregion

    #region Controls Region

    #endregion

    #region Sound Region
    private void SetMasterVolume(float value)
    {
        //Debug.Log($"Master Volume: {value}");
    }

    private void SetMusicVolume(float value)
    {
        //Debug.Log($"Music Volume: {value}");
    }

    private void SetSoundVolume(float value)
    {
        //Debug.Log($"Sound Volume: {value}");
    }

    #endregion
}
