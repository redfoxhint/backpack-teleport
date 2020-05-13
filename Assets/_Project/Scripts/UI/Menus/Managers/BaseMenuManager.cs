using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseMenuManager : MonoBehaviour
{
    [Header("Base Menu Configuration")]
    [SerializeField] private Button backButton;
    [SerializeField] private RectTransform defaultScreen;

    // Private Variables
    private RectTransform currentScreen;
    private List<Button> registeredButtons = new List<Button>();

    // Events

    // Properties
    public Stack<RectTransform> Screens { get; } = new Stack<RectTransform>();

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
        if(backButton != null)
        {
            RegisterButton(backButton, OnBackButtonClicked);
        }
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
        EnableBackButton();
    }

    private void GotoDefaultScreen()
    {
        GotoScreen(defaultScreen);

        Screens.Clear();
        DisableBackButton();
    }

    private void GotoScreen(RectTransform screenToGoto)
    {
        RectTransform oldScreen = currentScreen;
        currentScreen = screenToGoto;

        if (!Screens.Contains(oldScreen))
        {
            Screens.Push(oldScreen);
        }

        DisableScreen(oldScreen);
        EnableScreen(screenToGoto);
    }

    private void GoToPreviousScreen(RectTransform screenToGoto)
    {
        RectTransform oldScreen = currentScreen;
        currentScreen = screenToGoto;

        DisableScreen(oldScreen);
        EnableScreen(screenToGoto);
    }

    private void SetDefaultScreen()
    {
        bool isAlreadyEnabled = defaultScreen.gameObject.activeSelf;

        if (!isAlreadyEnabled)
        {
            EnableScreen(defaultScreen);
        }

        currentScreen = defaultScreen;
        Screens.Clear();
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
        if (Screens.Count == 0 || Screens == null) return;

        if (Screens.Peek().Equals(defaultScreen))
        {
            GotoDefaultScreen();
            return;
        }
        else if (Screens.Count > 1)
        {
            RectTransform previous = Screens.Pop();
            GoToPreviousScreen(previous);
        }
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

#if UNITY_EDITOR
[CustomEditor(typeof(MainMenuManager))]
public class StackPreview : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var baseMenu = (MainMenuManager)target;
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
#endif


