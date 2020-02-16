using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    [SerializeField] private GameObject keybindOptionPrefab;
    [SerializeField] private Transform keybindParent;

    [Space]

    [Header("Gameplay Options")]
    [SerializeField] private Toggle doGodModeToggle;  // FOR TESTING
    [SerializeField] private Toggle doSuperSpeedToggle; // FOR TESTING
    [SerializeField] private Toggle doNoStaminaToggle; // FOR TESTING

    [Header("Graphics Options")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vSyncToggle;
    [SerializeField] private Slider testSlider;

    [Header("Control Options")]


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
        CreateKeyBindOptions();
        LoadOptions();
    }

    private void InitializeButtons()
    {
        // Menu Buttons
        if (saveButton != null) saveButton.onClick.AddListener(OnSaveClicked);

        // Gameplay Options
        if (doGodModeToggle != null) doGodModeToggle.onValueChanged.AddListener(delegate { SetGodMode(doGodModeToggle.isOn); });
        if (doSuperSpeedToggle != null) doSuperSpeedToggle.onValueChanged.AddListener(delegate { SetSuperSpeed(doSuperSpeedToggle.isOn); });
        if (doNoStaminaToggle != null) doNoStaminaToggle.onValueChanged.AddListener(delegate { SetNoStamina(doNoStaminaToggle.isOn); });

        // Graphics Options
        if (resolutionDropdown != null) resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
        if (fullscreenToggle != null) fullscreenToggle.onValueChanged.AddListener(delegate { SetFullscreen(fullscreenToggle.isOn); });
        if (vSyncToggle != null) vSyncToggle.onValueChanged.AddListener(delegate { ToggleVsync(vSyncToggle.isOn); });
        if (testSlider != null) testSlider.onValueChanged.AddListener(delegate { SetTestSlider(testSlider.value); });

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
                doGodModeToggle.isOn,
                doSuperSpeedToggle.isOn,
                doNoStaminaToggle.isOn,
                fullscreenToggle.isOn,
                vSyncToggle.isOn,
                testSlider.value,
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
            doGodModeToggle.isOn = options.godMode;
            doSuperSpeedToggle.isOn = options.superSpeed;
            doNoStaminaToggle.isOn = options.noStamina;
            fullscreenToggle.isOn = options.fullScreen;
            vSyncToggle.isOn = options.vSync;
            testSlider.value = options.testValue;
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
        Debug.Log($"Test Slider: {value}");
    }

    #endregion

    #region Controls Region

    private void CreateKeyBindOptions()
    {
        Keybinds keybinds = InputManager.Instance.keybinds;

        foreach (KeyValuePair<string, Keybind> key in keybinds.keys)
        {
            GameObject newKey = Instantiate(keybindOptionPrefab, keybindParent);
            KeybindUI bindButton = newKey.GetComponentInChildren<KeybindUI>();

            bindButton.functionText.SetText(key.Value.keybindName);
            bindButton.bindText.SetText(key.Value.associatedKey.ToString());

            bindButton.keyBindButton.onClick.AddListener(delegate { ChangeKey(key.Value, bindButton.bindText); });
        }
    }

    private void ChangeKey(Keybind key, TextMeshProUGUI bindText)
    {
        StartCoroutine(ChangeKeyRoutine(key, bindText));
    }

    private IEnumerator ChangeKeyRoutine(Keybind key, TextMeshProUGUI bindText)
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                yield break;
            }

            if (Input.anyKeyDown)
            {
                Debug.Log(Input.inputString);
                UpdateKeyBind(key, bindText, Input.inputString);
                yield break;
            }

            yield return null;
        }
    }

    private void UpdateKeyBind(Keybind key, TextMeshProUGUI bindText, string inputString)
    {
        KeyCode inputToKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), inputString.ToUpper());

        key.associatedKey = inputToKey;
        bindText.SetText(inputString.ToUpper());
        InputManager.Instance.keybinds.UpdateKey(key);
    }

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
