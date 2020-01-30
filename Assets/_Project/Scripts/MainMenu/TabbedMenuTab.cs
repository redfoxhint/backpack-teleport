using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class TabbedMenuTab : MonoBehaviour
{
    [Header("Tab UI Elements")]
    [HideInInspector] public Button tabButton;
    [SerializeField] private Sprite selectedSprite;

    [Header("Other")]
    [SerializeField] private Transform tabContent;

    // Private Variables
    private TabbedMenu menu;
    private Sprite originalSprite;
    private bool selected = false;

    private void Awake()
    {
        tabButton = GetComponent<Button>();
        originalSprite = tabButton.image.sprite;
    }

    public void Init(TabbedMenu parentMenu)
    {
        menu = parentMenu;
        tabButton.onClick.AddListener(OnClicked);
    }

    public void ShowContent()
    {
        tabContent.gameObject.SetActive(true);
    }

    public void HideContent()
    {
        tabContent.gameObject.SetActive(false);
    }

    private void OnClicked()
    {
        menu.SetCurrentlySelectedTab(this);
    }

    public void Select()
    {
        selected = true;
        tabButton.image.sprite = selectedSprite;
        ShowContent();
    }

    public void Deselect()
    {
        selected = false;
        tabButton.image.sprite = originalSprite;
        HideContent();
    }
}
