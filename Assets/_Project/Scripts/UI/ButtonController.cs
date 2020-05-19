using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Button Sound Configuration")]
    [SerializeField] private AudioFiles buttonHoverSound = AudioFiles.SFX_Select1;
    [SerializeField] private AudioFiles buttonClickSound = AudioFiles.SFX_Click1;
    [SerializeField] private bool doSoundFeedback = true;

    [Header("Button Animation Configuration")]
    [SerializeField] private Vector3 hoverSize = new Vector3(1.1f, 1.1f, 1f);

    private Image buttonImage;

    private void OnEnable()
    {
        buttonImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlayUISoundEffect(buttonClickSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(doSoundFeedback)
            AudioManager.Instance.PlayUISoundEffect(buttonHoverSound);

        buttonImage.transform.DOScale(hoverSize, 0.1f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f).SetUpdate(true);
    }
}
