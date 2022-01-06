using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Crane
{


public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;
	private int _dialoguesCounter = 0;
	private Queue<string> _sentences;
	public CraneDialogue craneDialogue;
	public GameObject buttonA;
	

	// Use this for initialization
	void Start () {
		_sentences = new Queue<string>();
		TriggerDialogue();
		Time.timeScale = 0.0000000000000000000000000000000000000000000001f;
	}
	
	
	
	

	private void TriggerDialogue()
	{
		
		craneDialogue.TriggerDialogue(_dialoguesCounter);
		if (_dialoguesCounter == 1)
		{
			buttonA.SetActive(true);
		

			//StartCoroutine(WaitForKeyYoBePressed1());
			

		}
		
		
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
		//StartCoroutine(Wait1Second());
		Invoke("TriggerDialogue", 1*Time.timeScale);
		
		

		if (_dialoguesCounter ==2)
		{
			Time.timeScale = 1f;
			Invoke("DisplayNextSentence", 1.5f);
		}

		

	}
	IEnumerator WaitForKeyYoBePressed1()

		//do stuff

		{

			bool keyApressed = false;
			bool keyDpressed = false;
			bool keySpacepressed = false;
			
			
			
			//do stuff
 
			//wait for space to be pressed
			while(!keyApressed || !keyDpressed || !keySpacepressed)
			{
				if (Input.GetKeyDown(KeyCode.A))
				{
					keyApressed = true;
					buttonA.GetComponent<Image>().color = Color.green;
				}
				
				Debug.Log(keyApressed);
				Debug.Log(keyDpressed);
				Debug.Log(keySpacepressed);
				yield return null;
			}
 
			
			DisplayNextSentence();
			
			//do stuff once space is pressed
 
		}


		
	IEnumerator Wait1Second()
	{
		
		yield return new WaitForSeconds(1f*Time.timeScale);

	}

}
}