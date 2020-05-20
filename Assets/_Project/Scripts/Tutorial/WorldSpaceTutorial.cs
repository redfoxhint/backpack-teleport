using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceTutorial : MonoBehaviour
{
    [SerializeField] private WorldSpaceTutorialData tutorialData;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private GameObject buttonPressSpritePrefab;
    [SerializeField] private RectTransform panel;

    private bool firstDiscover;
    private List<Image> buttonImages = new List<Image>();

    private void Awake()
    {
        Init();
        FadeOut();
    }

    private void Init()
    {
        if (tutorialData == null)
        {
            Debug.Log("No tutorial data set.");
            return;
        }

        if(tutorialData.hasMultipleButtons)
        {
            foreach (Sprite sprite in tutorialData.buttonPressSprites)
            {
                Image newButtonSprite = Instantiate(buttonPressSpritePrefab, panel).GetComponent<Image>();
                newButtonSprite.sprite = sprite;
                newButtonSprite.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f).SetLoops(-1, LoopType.Yoyo);
                buttonImages.Add(newButtonSprite);
            }
        }

        tutorialText.SetText(tutorialData.tutorialText);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            FadeIn();

            if(!firstDiscover)
            {
                AudioManager.Instance.PlaySoundEffect(AudioFiles.SFX_Notification2);
                firstDiscover = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FadeOut();
        }
    }

    private void FadeIn()
    {
        foreach(Image buttonImage in buttonImages)
        {
            buttonImage.DOFade(1f, 1f);
        }
        
        tutorialText.DOFade(1f, 1f);
    }

    private void FadeOut()
    {
        foreach (Image buttonImage in buttonImages)
        {
            buttonImage.DOFade(0f, 1f);
        }
            
        tutorialText.DOFade(0f, 1f);
    }
}
