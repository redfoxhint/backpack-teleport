using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [Header("Selector UI")]
    [SerializeField] private Image levelPreviewImage;
    [SerializeField] private Image borderImage;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite originalSprite;

    public LevelData levelData;
    private LevelSelectionBar selectionBar;
    private bool selected;
    private bool levelUnlocked;

    private Button button;

    private void Awake()
    {
        selectionBar = GetComponentInParent<LevelSelectionBar>();
        button = GetComponent<Button>();
        selectedSprite = button.spriteState.highlightedSprite;
        originalSprite = borderImage.sprite;

        Initialize();
    }

    public void Initialize()
    {
        if (levelData == null)
        {
            levelPreviewImage.color = Color.clear;
            DisableSelector();
            return;
        }

        EnableSelector();
        levelPreviewImage.sprite = levelData.levelPreview;

        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        if (levelData == null) return;

        selectionBar.SetCurrentLevelSelected(this);
    }

    public void Select()
    {
        selected = true;
        borderImage.sprite = selectedSprite;
    }

    public void Deselect()
    {
        selected = false;
        borderImage.sprite = originalSprite;
    }

    public void EnableSelector()
    {
        button.interactable = true;
        borderImage.color = Color.white;
        //borderImage.sprite = unlockedSprite;
    }
    public void DisableSelector()
    {
        button.interactable = false;
        //borderImage.sprite = lockedSprite;
    }
}
