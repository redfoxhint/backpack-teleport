using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite hoveredSprite;
    private Button button;
    private TextMeshProUGUI buttonText;

    private Sprite originalImage;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        originalImage = button.image.sprite;

        ConfigureButton();
    }

    private void ConfigureButton()
    {
        button.transition = Selectable.Transition.SpriteSwap;
    }

    public void SetButtonText(string newText)
    {
        buttonText.SetText(newText);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    private void SetButtonImage()
    {

    }
}
