using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] private RectTransform endScreenPanel;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        quitButton.onClick.AddListener(OnQuitClicked);        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            CameraFunctions.Instance.SetScreenColor(Color.black);
            GameManager.Instance.PauseGame();
            endScreenPanel.gameObject.SetActive(true);
        }
    }

    private void OnQuitClicked()
    {
        SceneLoader.Instance.LoadLevel(1);
        GameManager.Instance.ResumeGame();
        endScreenPanel.gameObject.SetActive(false);
    }
}
