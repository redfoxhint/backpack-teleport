using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseMenu : MonoBehaviour
{
    [Header("Base Menu Configuration")]
    [SerializeField] private Button backButton;
    [SerializeField] private RectTransform defaultScreen;

    // Private Variables
    protected RectTransform previousScreen;
    private List<Button> registeredButtons = new List<Button>();

    // Events

    // Properties
    public Stack<RectTransform> Screens { get; } = new Stack<RectTransform>();
    protected RectTransform CurrentScreen
    {
        get => CurrentScreen;
        set
        {
            if(value.Equals(CurrentScreen))
            {
                return;
            }

            CurrentScreen = value;
        }
    }


    protected virtual void Awake()
    {
        InitializeMenu();
    }

    private void OnDisable()
    {
        UnregisterAllButtons();
    }

    protected virtual void InitializeMenu()
    {
        InitializeButtons();
        SetDefaultScreen();
    }

    protected virtual void InitializeButtons()
    {
        RegisterButton(backButton, OnBackButtonClicked);
    }

    protected void OverrideBackButton(Button newButton)
    {
        UnregisterButton(backButton);
        backButton = newButton;
        RegisterButton(backButton, OnBackButtonClicked);
    }

    protected void OverrideBackButtonAction(UnityAction newAction)
    {
        if (backButton == null) return;
        UnregisterButton(backButton);
        RegisterButton(backButton, newAction);
    }

    protected void UpdateCurrentScreen(RectTransform newScreen)
    {
        GotoScreen(newScreen);
    }

    private void GotoScreen(RectTransform screenToGoto)
    {
        if(CurrentScreen != null)
        {
            DisableScreen(CurrentScreen);
            previousScreen = CurrentScreen;
        }
        else
        {
            previousScreen = null;
        }

        EnableScreen(screenToGoto);
        CurrentScreen = screenToGoto;
        EnableBackButton();
    }
    private void GoToPreviousScreen()
    {
        RectTransform previousInStack = Screens.Pop();

        if(previousScreen == null)
        {
            DisableBackButton();
            return;
        }

        GotoScreen(previousScreen);
        previousScreen = previousInStack;
    }

    private void SetDefaultScreen()
    {
        CurrentScreen = defaultScreen;
    }

    private void EnableScreen(RectTransform screenToEnable)
    {
        if (screenToEnable != null)
        {
            screenToEnable.gameObject.SetActive(true);
        }
    }

    private void DisableScreen(RectTransform screenToDisable)
    {
        if (screenToDisable != null)
        {
            screenToDisable.gameObject.SetActive(false);
        }
    }
    protected void RegisterButton(Button buttonToRegister, UnityAction methodToSubscribeTo)
    {
        if (buttonToRegister != null)
        {
            buttonToRegister.onClick.AddListener(methodToSubscribeTo);
            registeredButtons.Add(buttonToRegister);
        }
    }

    private void UnregisterButton(Button buttonToUnregister)
    {
        if (buttonToUnregister == null) return;
        buttonToUnregister.onClick.RemoveAllListeners();
        registeredButtons.Remove(buttonToUnregister);
    }

    private void UnregisterAllButtons()
    {
        if (registeredButtons.Count == 0 || registeredButtons == null) return;

        foreach (Button registeredButton in registeredButtons)
        {
            registeredButton.onClick.RemoveAllListeners();
        }
    }

    private void OnBackButtonClicked()
    {
        GoToPreviousScreen();
    }

    private void EnableBackButton()
    {
        if (backButton != null)
        {
            backButton.gameObject.SetActive(true);
        }
    }

    private void DisableBackButton()
    {
        if (backButton != null)
        {
            backButton.gameObject.SetActive(false);
        }
    }
}

[CustomEditor(typeof(PauseMenuManager))]
public class StackPreview : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var baseMenu = (PauseMenuManager)target;
        var stack = baseMenu.Screens;

        var bold = new GUIStyle();
        bold.fontStyle = FontStyle.Bold;
        GUILayout.Label("Screens stack", bold);

        foreach (var screen in stack)
        {
            GUILayout.Label(screen.name);
            Repaint();
        }
    }
}
