using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crane
{
	

[System.Serializable]

public class CraneDialogue : MonoBehaviour {
	
	public Dialogue[] dialogues;

	public Dialogue dialogue1;
	public Dialogue dialogue2;
	public Dialogue dialogue3;


	public void TriggerDialogue(int dialogueNumber)
	{
		if (dialogues.Length >= dialogueNumber)
		{
			FindObjectOfType<DialogueManager>().StartDialogue(dialogues[dialogueNumber]);
		}
		
	}
	public void TriggerDialogue1 ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
	}
	public void TriggerDialogue2 ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue2);
	}
	public void TriggerDialogue3 ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue3);
	}

	}

}
