using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Selector UI")]
    [SerializeField] private Image levelPreviewImage;
    [SerializeField] private Image borderImage;

    public LevelData levelData;
    private LevelSelectionBar selectionBar;

    private Sprite hoveredImage;
    private Sprite originalImage;
    private Button button;
    private bool selectable;
    private bool selected;

    private void Awake()
    {
        hoveredImage = Resources.Load<Sprite>("Sprites/UI/Menus/Story Menu/storymenu_level_box_outline_hovered");
        selectionBar = GetComponentInParent<LevelSelectionBar>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
        originalImage = borderImage.sprite;

        Initialize();
    }

    private void OnClicked()
    {
        if (levelData == null) return;

        selectionBar.SetCurrentLevelSelected(this);
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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selected) return;
        if (!selectable) return;

        SetBorderImage(hoveredImage);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selected) return;
        if (!selectable) return;

        SetBorderImage(originalImage);
    }

    private void SetBorderImage(Sprite border)
    {
        borderImage.sprite = border;
    }

    public void Select()
    {
        SetBorderImage(hoveredImage);
        selected = true;
    }

    public void Deselect()
    {
        SetBorderImage(originalImage);
        selected = false;
    }

    public void EnableSelector()
    {
        button.interactable = true;
        borderImage.color = Color.white;
        selectable = true;
    }
    public void DisableSelector()
    {
        button.interactable = false;
        borderImage.color = new Color(34, 34, 34, 29);
        selectable = false;
    }
}
