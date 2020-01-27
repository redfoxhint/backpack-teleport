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
    [SerializeField] private Button demoButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button continueButton;

    private void Awake()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        if (demoButton != null) demoButton.onClick.AddListener(OnDemoButtonClicked);
        if (newGameButton != null) newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        if (loadGameButton != null) loadGameButton.onClick.AddListener(OnLoadGameButtonClicked);
        if (continueButton != null) continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    private void OnDemoButtonClicked()
    {
        SceneManager.LoadScene(1);
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
