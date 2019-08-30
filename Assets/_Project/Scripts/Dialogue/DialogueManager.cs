using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BackpackTeleport.Dialogue
{
	public class DialogueManager : MonoBehaviour
	{
		#region Singleton
		private static DialogueManager instance;
		public static DialogueManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<DialogueManager>();
				}
				return instance;
			}
		}

		#endregion

		// Public Variables
		[SerializeField] private TextMeshProUGUI dialogueText;

		// Private Variables
		private Queue<string> sentences;
		private bool hasSentence;
		private int sentencesCount;

		// Components
		[SerializeField] private Animator animator;

		// Tasks
		private Task sentenceTypeTask;
		private Task endDialogueTask;

		private void Awake()
		{
			sentences = new Queue<string>();
		}

		private void Update()
		{
			sentencesCount = sentences.Count;
		}

		private void StartTypingSentence(string sentence)
		{
			if (sentenceTypeTask != null) sentenceTypeTask.Stop();

			sentenceTypeTask = new Task(TypeSentence(sentence));
			sentenceTypeTask.Start();
		}

		public void StartDialogue(Dialogue dialogue)
		{
			animator.SetBool("IsOpen", true);
			hasSentence = true;

			sentences.Clear();

			foreach (string sentence in dialogue.instructions)
			{
				sentences.Enqueue(sentence);
			}

			DisplayNextSentence();
		}

		public void StartEndDialogue()
		{
			if (endDialogueTask != null) endDialogueTask.Stop();

			endDialogueTask = new Task(EndDialogue());
		}

		public void DisplayNextSentence()
		{
			if (sentences.Count == 1)
			{
				StartEndDialogue();
				return;
			}

			string sentence = sentences.Dequeue();
			StartTypingSentence(sentence);
		}

		IEnumerator TypeSentence(string sentence)
		{
			dialogueText.SetText("");
			foreach (char letter in sentence.ToCharArray())
			{
				dialogueText.text += letter;
				yield return null;
			}
		}

		IEnumerator EndDialogue()
		{
			string sentence = sentences.Dequeue();
			StartTypingSentence(sentence);

			yield return new WaitForSeconds(3f);

			animator.SetBool("IsOpen", false);
		}
	}
}


