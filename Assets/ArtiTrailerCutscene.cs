using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtiTrailerCutscene : MonoBehaviour
{
    [SerializeField] private Animator cloneAnimator;
    [SerializeField] private Animator enemyCloneAnimator;
    [SerializeField] private Transform artiClone;
    [SerializeField] private Transform cameraTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.PlayerControl = false;
            StartCoroutine(CutsceneRoutine(other));
        }
    }

    private IEnumerator CutsceneRoutine(Collider2D other)
    {
        artiClone.transform.position = other.transform.position;
        other.GetComponent<SpriteRenderer>().enabled = false;
        artiClone.gameObject.SetActive(true);
        cloneAnimator.enabled = true;

        

        yield return new WaitForSeconds(2f);
        CameraFunctions.Instance.FollowTarget = cameraTarget;

        yield return new WaitForSeconds(1f);
        enemyCloneAnimator.enabled = true;
    }
}
