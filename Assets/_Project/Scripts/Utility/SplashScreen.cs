using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private bool isPlayingIntro;

    private void Awake()
    {
        videoPlayer.loopPointReached += LoadNextLevel;
        videoPlayer.Play();
        isPlayingIntro = true;
    }

    private void LoadNextLevel(VideoPlayer source)
    {
        SceneLoader.Instance.LoadLevel(1);
    }

    private void Update()
    {
        if (isPlayingIntro)
        {
            if(Keyboard.current == null && Gamepad.current == null)
            {
                LogUtils.LogError("No input device detected. Ignoring skip intro input.");
                return;
            }

            if(Keyboard.current.anyKey.wasPressedThisFrame)
            {
                LoadNextLevel(videoPlayer);
                isPlayingIntro = false;
            }
        }
    }
}
