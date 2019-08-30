using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BackpackTeleport.Dialogue;
using BackpackTeleport.Events;

public class TutorialManager : MonoBehaviour
{
	[SerializeField] private TutorialSegment teleportTutorial;

	private bool tutorialStarted = false;
	private bool firstThrow = false;
	private bool firstTeleport = false;
	private bool markersPlaced = false;
	private int markersPlacedCount = 0;

	// Tasks
	private Task delayNextSentenceTask;

	private void Start()
	{
		DialogueManager.Instance.StartDialogue(teleportTutorial.tutorialDialogue);
		tutorialStarted = true;
	}

	private void StartDelay(float delay)
	{
		if (delayNextSentenceTask != null)
		{
			delayNextSentenceTask.Stop();
			delayNextSentenceTask = null;
		}

		delayNextSentenceTask = new Task(DelayNextSentence(delay));
	}

	public void CompleteFirstThrow()
	{
		if (firstThrow) return;

		firstThrow = true;
		StartDelay(0f);
		//DialogueManager.Instance.DisplayNextSentence();
	}

	public void CompleteFirstTeleport()
	{
		if (firstTeleport) return;
		if(!firstThrow) return;

		firstTeleport = true;
		DialogueManager.Instance.DisplayNextSentence();
		StartDelay(1f);
	}

	public void CompletePlacingMarkers()
	{
		if(!firstTeleport) return;

		if (markersPlaced) return;

		if (markersPlacedCount + 1 >= 2)
		{
			markersPlaced = true;
			StartDelay(0f);
		}
		else
		{
			markersPlacedCount++;
		}
	}

	IEnumerator DelayNextSentence(float delay)
	{
		yield return new WaitForSeconds(delay);
		DialogueManager.Instance.DisplayNextSentence();
		yield break;
	}
}
