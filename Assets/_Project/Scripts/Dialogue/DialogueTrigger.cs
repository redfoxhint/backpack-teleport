using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackpackTeleport.Dialogue
{
	public class DialogueTrigger : MonoBehaviour
	{
		public Dialogue dialogue;

		public void TriggerDialogue()
		{
			DialogueManager.Instance.StartDialogue(dialogue);
		}
	}
}


