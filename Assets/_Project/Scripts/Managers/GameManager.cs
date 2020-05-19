using BackpackTeleport.Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
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

    private Player player;
    private Camera mainCam;

    public int berryAmount = 0;

    private void Start()
    {
        player = Player;
        mainCam = Camera.main;
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
