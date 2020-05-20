﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnScreenController : Singleton<RespawnScreenController>
{
    [SerializeField] private Button respawnButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private RectTransform deathScreen;
    [SerializeField] private Color deathScreenColor = Color.red;

    private void Awake()
    {
        respawnButton.onClick.AddListener(OnRespawnClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }

    private void Start()
    {
        deathScreen.gameObject.SetActive(false);
    }

    public void ShowRespawnScreen()
    {
        GameManager.Instance.PauseGame();

        CameraFunctions.Instance.FadeDOFIn();
        CameraFunctions.Instance.SetScreenColor(deathScreenColor);
        deathScreen.gameObject.SetActive(true);
        Cursor.visible = true;
    }

    public void OnRespawnClicked()
    {
        SceneLoader.Instance.LoadLevel(AudioFiles.ST_RelaxingNature);
        GameManager.Instance.ResumeGame();
        deathScreen.gameObject.SetActive(false);
    }

    public void OnMainMenuClicked()
    {
        SceneLoader.Instance.LoadLevel(1);
        GameManager.Instance.ResumeGame();
        deathScreen.gameObject.SetActive(false);
    }
}