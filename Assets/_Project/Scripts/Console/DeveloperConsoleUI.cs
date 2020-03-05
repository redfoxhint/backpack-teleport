using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DeveloperConsoleUI : MonoBehaviour
{
    [SerializeField] private GameObject consoleCanvas;
    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private TMP_InputField inputField;

    private bool didShow = false;

    private DeveloperConsole console = new DeveloperConsole();

    //private void Awake()
    //{
    //    Application.logMessageReceived += HandleUnityMessage;
    //}

    private void Start()
    {
        if (console != null)
        {
            console.visibilityChanged += OnVisibilityChanged;
            console.logChanged += OnLogChanged;
        }
    }

    ~DeveloperConsoleUI()
    {
        console.visibilityChanged -= OnVisibilityChanged;
        console.logChanged -= OnLogChanged;
        //Application.logMessageReceived -= HandleUnityMessage;
    }

    private void Update()
    {
        // TODO: Convert this to use new input system

        //if (InputManager.Instance.KeyDown("toggleConsole"))
        //{
        //    ToggleVisibility();
        //}

        //if (InputManager.Instance.KeyDown("confirm"))
        //{
        //    RunCommand();
        //}
    }

    private void HandleUnityMessage(string condition, string stackTrace, LogType type)
    {
        string message = $"[ {type.ToString()} ] {condition}";
        OnLogChanged(message);
    }

    public void ToggleVisibility()
    {
        SetVisibility(!consoleCanvas.activeSelf);
    }

    private void SetVisibility(bool visibilty)
    {
        consoleCanvas.SetActive(visibilty);

        if (visibilty == true)
        {
            inputField.ActivateInputField();
        }
    }

    private void UpdateLogString(string newLog)
    {
        if (newLog == null)
        {
            return;
        }
        else
        {
            logText.text += $"\n{newLog}";
        }
    }

    public void RunCommand()
    {
        console.RunCommandString(inputField.text);
        inputField.text = "";
        inputField.ActivateInputField();
    }

    private void OnLogChanged(string log)
    {
        UpdateLogString(log);
    }

    private void OnVisibilityChanged(bool visible)
    {
        SetVisibility(visible);
    }
}
