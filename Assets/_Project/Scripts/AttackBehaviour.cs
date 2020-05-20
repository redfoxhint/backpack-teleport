using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackBehaviour : StateMachineBehaviour
{
    private AttackManager attackManager;
    private PhysicsCharacterController playerMovement;

     //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackManager = animator.GetComponent<AttackManager>();
        playerMovement = animator.GetComponent<PhysicsCharacterController>();

        GameManager.Instance.PlayerControl = false;

        Debug.Log("Started attack animation");

        if(Gamepad.current != null)
        {
            InputSystem.ResumeHaptics();
            Gamepad.current.SetMotorSpeeds(0.25f, 0.75f);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.PlayerControl = true;

        Debug.Log("Finished attack animation");

        if (Gamepad.current != null)
        {
            InputSystem.PauseHaptics();
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
