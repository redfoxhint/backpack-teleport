using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    [SerializeField] private CanvasGroup fadeCanvas;

    [Header("Fade Configuration")]
    [SerializeField] private float fadeTime = 1f;

    public override void Awake()
    {
        base.Awake();

        SceneManager.sceneLoaded += OnSceneLoaded;

        FadeCanvasImmediate();
        FadeToClear();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            FadeToClear();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FadeToBlack();
        }
    }

    public void LoadLevel(string levelName, AudioFiles audioToCrossfadeInto = AudioFiles.Nothing)
    {
        string sceneName = levelName.Replace(' ', '_');

        Sequence loadLevelSequence = DOTween.Sequence();
        loadLevelSequence.AppendCallback(FadeToBlack);
        loadLevelSequence.AppendInterval(fadeTime + 0.5f).OnComplete(() => StartCoroutine(LoadLevelAsyncRoutine(sceneName, audioToCrossfadeInto)));
    }

    public void LoadLevel(int levelIndex, AudioFiles audioToCrossfadeInto = AudioFiles.Nothing)
    {
        Sequence loadLevelSequence = DOTween.Sequence();
        loadLevelSequence.AppendCallback(FadeToBlack);
        loadLevelSequence.AppendInterval(fadeTime + 0.5f).OnComplete(() => StartCoroutine(LoadLevelAsyncRoutine(levelIndex, audioToCrossfadeInto)));
    }

    IEnumerator LoadLevelAsyncRoutine(string sceneToLoad, AudioFiles audioToFadeInto)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        if(audioToFadeInto != AudioFiles.Nothing)
        {
            AudioManager.Instance.Crossfade(AudioType.MUSIC, audioToFadeInto);
        }

        //FadeToClear();
        yield break;
    }

    IEnumerator LoadLevelAsyncRoutine(int sceneToLoad, AudioFiles audioToFadeInto)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (audioToFadeInto != AudioFiles.Nothing)
        {
            AudioManager.Instance.Crossfade(AudioType.MUSIC, audioToFadeInto);
        }

        //FadeToClear();
        yield break;
    }

    private void FadeToBlack()
    {
        if (fadeCanvas == null) return;

        DOVirtual.Float(fadeCanvas.alpha, 1, fadeTime, (amount) =>
        {
            fadeCanvas.alpha = amount;
        }).OnComplete(() =>
        {
            fadeCanvas.blocksRaycasts = true;
            fadeCanvas.interactable = false;
        });
    }

    private void FadeToClear()
    {
        if (fadeCanvas == null) return;

        DOVirtual.Float(fadeCanvas.alpha, 0, fadeTime, (amount) =>
        {
            fadeCanvas.alpha = amount;
        }).OnComplete(() =>
        {
            fadeCanvas.blocksRaycasts = false;
            fadeCanvas.interactable = true;
        });
    }
    private void FadeCanvasImmediate()
    {
        if (fadeCanvas == null) return;

        fadeCanvas.alpha = 1;
        fadeCanvas.blocksRaycasts = true;
        fadeCanvas.interactable = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        FadeToClear();
    }
}
