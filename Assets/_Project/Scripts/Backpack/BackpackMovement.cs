using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackpackMovement : BaseObjectMovement, IActivator
{
    // Public Variables
    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; }

    private Backpack backpack;

    protected override void Awake()
    {
        base.Awake();
        careAboutAnimator = false;
        backpack = GetComponent<Backpack>();
    }

    public void MoveToPoint(Vector2 target, bool useDefaultSpeed, float speed = 0, TweenCallback onMoveFinishedCallback = null)
    {
        float spd = useDefaultSpeed ? moveSpeed : speed;

        rBody.DOMove(target, spd, false).OnComplete(onMoveFinishedCallback);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        CancelMovement();
    }

    public void CancelMovement()
    {
        rBody.DOKill(true);
        rBody.velocity = Vector2.zero;
        //backpack.UpdateTeleportationDestination(backpack.transform.position);
        FixPosition();
    }
}
