using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float transitionTime = 1;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadLevelByIndex(int index)
    {
        Scene scene = SceneManager.GetSceneByBuildIndex(index);

        if (scene == null)
        {
            Debug.LogError($"Scene at index {index} was not found.");
            return;
        }

        StartCoroutine(LoadLevel(index));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
