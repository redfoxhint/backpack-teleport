using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer.loopPointReached += LoadNextLevel;
    }

    private void LoadNextLevel(VideoPlayer source)
    {
        SceneLoader levelLoader = FindObjectOfType<SceneLoader>();
        levelLoader.LoadNextLevel();
    }
}
