using BackpackTeleport.Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private bool playerControl;
    [SerializeField] private bool firstPickup = true;

    public bool PlayerControl { get => playerControl; set => playerControl = value; } 
    public bool FirstPickup { get => firstPickup; set => firstPickup = value; }
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(berryAmount > 0)
            {
                Player.GetComponent<PlayerDamageable>().AddHealth(1f);
                berryAmount--;
            }
        }
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
