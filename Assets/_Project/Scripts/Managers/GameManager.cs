using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public bool GamePaused { get; set; }

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
