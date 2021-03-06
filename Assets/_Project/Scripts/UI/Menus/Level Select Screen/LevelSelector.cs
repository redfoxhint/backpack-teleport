﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [Header("Selector UI")]
    [SerializeField] private Image levelPreviewImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite selectedSprite;

    public LevelData levelData;
    private bool selected;
    private Sprite originalBackgroundImage;

    private void Awake()
    {
        Initialize();
        originalBackgroundImage = backgroundImage.sprite;
    }

    public void Initialize()
    {
        if (levelData == null)
        {
            levelPreviewImage.color = Color.clear;
            return;
        }

        levelPreviewImage.sprite = levelData.levelPreview;
    }

    public void UnlockLevel()
    {
        if (levelData == null) return;

        levelData.levelLocked = false;
    }

    public void LockLevel()
    {
        if (levelData == null) return;

        levelData.levelLocked = true;
    }

    public void Select()
    {
        backgroundImage.sprite = selectedSprite;
    }

    public void Deselect()
    {
        backgroundImage.sprite = originalBackgroundImage;
    }
}
