using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * This class is will be responsible for animating playable or AI characters in the game.
 * 
    FACING DIRECTIONS:
    DOWN =      0
    RIGHT =     1
    LEFT =      2
    UPRIGHT =   3
    UP =        4
    DOWNRIGHT = 5
    UPLEFT =    6
    DOWNLEFT =  7

 */
[RequireComponent(typeof(Animator))]
public class CharacterBase : MonoBehaviour
{
    public Animator CharacterAnimator { get; private set; }
    public int FacingDirection { get; private set; }

    private void Awake()
    {
        CharacterAnimator = GetComponent<Animator>();
    }

    // Facing Direction Context
    private Vector2 down = new Vector2(0, -1);
    private Vector2 right = new Vector2(1, 0);
    private Vector2 left = new Vector2(-1, 0);
    private Vector2 upright = new Vector2(1f, 1f);
    private Vector2 up = new Vector2(0, 1);
    private Vector2 downright = new Vector2(1f, -1f);
    private Vector2 upleft = new Vector2(-1f, 1f);
    private Vector2 downleft = new Vector2(-1f, -1f);

    public void SetAnimatorParameters(Vector2 moveDir)
    {
        if (CharacterAnimator != null)
        {
            SetDirection(moveDir);

            CharacterAnimator.SetFloat("Horizontal", Mathf.RoundToInt(moveDir.x));
            CharacterAnimator.SetFloat("Vertical", Mathf.RoundToInt(moveDir.y));
            CharacterAnimator.SetFloat("Speed", moveDir.sqrMagnitude);
        }
    }

    private void SetFacing(float newDirection)
    {
        if (newDirection < 0 || newDirection > 7) return; // In case a number which is not a direction is passed in.
        CharacterAnimator.SetFloat("facingDirection", newDirection);
        FacingDirection = Mathf.RoundToInt(newDirection);
    }

    public void SetDirection(Vector2 moveDir)
    {
        if (moveDir == Vector2.zero) return; // To preserve last move direction

        moveDir = new Vector2(Mathf.RoundToInt(moveDir.x), Mathf.RoundToInt(moveDir.y));

        if (moveDir == down) SetFacing(0f);
        if (moveDir == right) SetFacing(1f);
        if (moveDir == left) SetFacing(2f);
        if (moveDir == upright) SetFacing(3f);
        if (moveDir == up) SetFacing(4f);
        if (moveDir == downright) SetFacing(5f);
        if (moveDir == upleft) SetFacing(6f);
        if (moveDir == downleft) SetFacing(7f);
    }
}
