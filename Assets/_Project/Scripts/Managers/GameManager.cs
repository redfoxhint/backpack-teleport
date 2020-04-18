using BackpackTeleport.Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public bool PlayerControl { get; set; } 
    public bool GamePaused { get; set; }
    public Player Player
    {
        get
        {
            if(player == null)
            {
                player = FindObjectOfType<Player>();
                if(player != null)
                {
                    LogUtils.Log("GameManager found reference to player.");
                    return player;
                }
                else
                {
                    return null;
                }
            }

            return player;
        }
    }

    public PerfectPixelWithZoom PixelPerfectCamera
    {
        get
        {
            if(pixelPerfectCamera == null)
            {
                pixelPerfectCamera = FindObjectOfType<PerfectPixelWithZoom>();
                if(pixelPerfectCamera != null)
                {
                    return pixelPerfectCamera;
                }
                else
                {
                    LogUtils.LogWarning("Pixel Perfect Camera not found.");
                    return null;
                }
            }

            return pixelPerfectCamera;
        }
    }

    private Player player;
    private PerfectPixelWithZoom pixelPerfectCamera;
    private Camera mainCam;

    private void Start()
    {
        player = Player;
        mainCam = Camera.main;
        pixelPerfectCamera = PixelPerfectCamera;
        PlayerControl = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        GamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        GamePaused = false;
    }
}
