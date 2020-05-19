using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceTutorial : MonoBehaviour
{
    [SerializeField] private WorldSpaceTutorialData tutorialData;
    [SerializeField] private Image buttonPressImage;
    [SerializeField] private TextMeshProUGUI tutorialText;

    private bool firstDiscover;

    private void Awake()
    {
        Init();
        FadeOut();
    }

    private void OnValidate()
    {
        Init();   
    }

    private void Init()
    {
        if (tutorialData == null)
        {
            Debug.Log("No tutorial data set.");
            return;
        }

        buttonPressImage.sprite = tutorialData.buttonPressSprite;
        tutorialText.SetText(tutorialData.tutorialText);

        buttonPressImage.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            FadeIn();

            if(!firstDiscover)
            {
                AudioManager.Instance.PlaySoundEffect(AudioFiles.SFX_TutorialFirstDiscover);
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
        buttonPressImage.DOFade(1f, 1f);
        tutorialText.DOFade(1f, 1f);
    }

    private void FadeOut()
    {
        buttonPressImage.DOFade(0f, 1f);
        tutorialText.DOFade(0f, 1f);
    }
}
