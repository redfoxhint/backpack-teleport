using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class DemoLevelMainMenu : MonoBehaviour
{
    [SerializeField] private Volume introVolume;
    [SerializeField] private Transform playerHUD;
    [SerializeField] private RectTransform titleImage;
    [SerializeField] private Image anyKeyImage;
    [SerializeField] private TextMeshProUGUI anyKeyText;
    [SerializeField] private ArtiCutsceneClone artiClone;

    // Sequence Settings
    [SerializeField] private float titleMoveTime = 1f;
    [SerializeField] private float clearDOFTime = 2f;
    [SerializeField] private float textFadeTime = 1f;

    private bool canStart = false;

    // Volume Profile
    private VolumeProfile profile;
    private DepthOfField dof;

    private void Awake()
    {
        if (introVolume != null)
        {
            profile = introVolume.profile;
            profile.TryGet<DepthOfField>(out dof);
        }
        else
        {
            LogUtils.LogWarning("Intro profile was not found! Make sure it is hooked up in the inspector.");
        }

        InitIntro();
    }

    private void Update()
    {
        if(canStart)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                // Start game
                StartGame();
                LogUtils.Log("Game started.");
            }
        }
    }

    private void InitIntro()
    {
        playerHUD.gameObject.SetActive(false);
        dof.focusDistance.value = 0;

        IntroSequence();
    }

    private void IntroSequence()
    {
        Sequence introSeq = DOTween.Sequence();

        introSeq.AppendCallback(MoveTitle);
        introSeq.AppendInterval(titleMoveTime);
        introSeq.AppendCallback(ClearDOF);
        introSeq.OnComplete(OnIntroSequenceFinish);
    }

    private void StartGame()
    {
        titleImage.GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 0f), 1f);
        anyKeyImage.DOColor(new Color(1f, 1f, 1f, 0f), 1f);
        anyKeyText.DOColor(new Color(1f, 1f, 1f, 0f), 1f);
        artiClone.StartGame();

        canStart = false;
    }

    private void OnIntroSequenceFinish()
    {
        AnimateText();
        anyKeyText.DOColor(Color.white, textFadeTime);
        anyKeyImage.DOColor(Color.white, 1f);
        canStart = true;
    }

    private void MoveTitle()
    {
        titleImage.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Vector3 yMove = new Vector3(titleImage.position.x, titleImage.position.y + 390f, 0f);
        titleImage.transform.DOMove(yMove, titleMoveTime, false);
    }

    private void ClearDOF()
    {
        DOVirtual.Float(0f, 10, clearDOFTime, (x) => 
        {
            dof.focusDistance.value = x;        
        });
    }

    private void AnimateText()
    {
        Vector3 newScale = new Vector3(1.1f, 1.1f, 1);
        anyKeyImage.transform.DOScale(newScale, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
