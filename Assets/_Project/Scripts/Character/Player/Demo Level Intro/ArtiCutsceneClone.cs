using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArtiCutsceneClone : MonoBehaviour
{
    [SerializeField] private GameObject realPlayer;
    [SerializeField] private SpriteRenderer playerShadowSpr;

    private SpriteRenderer playerSpriteRenderer;
    private Animator cloneAnimator;

    private bool canDoRandom = true;

    private void Awake()
    {
        playerSpriteRenderer = realPlayer.GetComponent<SpriteRenderer>();
        playerSpriteRenderer.enabled = false;
        cloneAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(RandomIdleRoutine());
    }

    public void StartGame()
    {
        cloneAnimator.SetTrigger("doTeleport");
    }

    private IEnumerator RandomIdleRoutine()
    {
        LogUtils.Log("Random Idle Routine started");

        while(canDoRandom)
        {
            int waitTime = Random.Range(4, 6);
            cloneAnimator.SetTrigger("doRandom");
            yield return new WaitForSeconds(waitTime);
        }
    }

    // Animation event called at end of intro teleport sprite.
    public void OnTeleportAnimDone()
    {
        realPlayer.transform.position = transform.position;
        playerSpriteRenderer.enabled = true;
        playerShadowSpr.enabled = true;
        GameManager.Instance.PlayerControl = true;
        gameObject.SetActive(false);
    }
}
