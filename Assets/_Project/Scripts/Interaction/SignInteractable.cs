using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInteractable : Interactable
{
    [SerializeField] private Tutorial testTutorial;

    public override void Interact()
    {
        TutorialDialogManager tutorialManager = FindObjectOfType<TutorialDialogManager>();

        if(tutorialManager != null)
        {
            tutorialManager.SetTutorial(testTutorial);
        }
    }
}
