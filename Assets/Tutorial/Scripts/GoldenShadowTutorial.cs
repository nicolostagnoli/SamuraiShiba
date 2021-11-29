using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenShadowTutorial : MonoBehaviour {
	
	public Dialogue[] dialogues;

	public Dialogue dialogue1;
	public Dialogue dialogue2;
	public Dialogue dialogue3;
	public Dialogue dialogue4;
	public Dialogue dialogue5;

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
	public void TriggerDialogue4 ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue4);
	}
	public void TriggerDialogue5 ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue5);
	}

}
