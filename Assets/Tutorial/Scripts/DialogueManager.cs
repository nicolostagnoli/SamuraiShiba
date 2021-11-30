using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;
	private int _dialoguesCounter = 0;
	private Queue<string> _sentences;
	public GoldenShadowTutorial goldenShadowTutorial;
	

	// Use this for initialization
	void Start () {
		_sentences = new Queue<string>();
		TriggerDialogue();
	}

	private void TriggerDialogue()
	{
		
		goldenShadowTutorial.TriggerDialogue(_dialoguesCounter);
		
	}
	

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			DisplayNextSentence();
		}
		
	}

	public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		_sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			_sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (_sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = _sentences.Dequeue();
		
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		_dialoguesCounter++;
		StartCoroutine(Wait1Second());
		Invoke("TriggerDialogue", 2);
		if (_dialoguesCounter ==5)
		{
			SceneManager.LoadScene("Level1");
		}

	}
	
	IEnumerator Wait1Second()
	{
		
		yield return new WaitForSeconds(10f);

	}

}
