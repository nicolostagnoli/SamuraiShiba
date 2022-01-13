using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
	public Text continueText;

	public Animator animator;
	private int _dialoguesCounter = 0;
	private Queue<string> _sentences;
	public GoldenShadowTutorial goldenShadowTutorial;
	public GameObject buttonA;
	public GameObject buttonD;
	public GameObject buttonJ;
	public GameObject buttonK;
	public GameObject buttonShift;
	public GameObject buttonSpacebar;

	// Use this for initialization
	void Start () {
		_sentences = new Queue<string>();
		TriggerDialogue();
	}

	private void TriggerDialogue()
	{
		
		goldenShadowTutorial.TriggerDialogue(_dialoguesCounter);
		if (_dialoguesCounter == 1)
		{
			buttonA.SetActive(true);
			buttonD.SetActive(true);
			buttonSpacebar.SetActive(true);
			continueText.text = "Press the keys below...";

			StartCoroutine(WaitForKeyYoBePressed1());
			

		}
		if (_dialoguesCounter == 2)
		{
			buttonA.SetActive(false);
			buttonD.SetActive(false);
			buttonSpacebar.SetActive(false);
			buttonJ.SetActive(true);
			buttonK.SetActive(true);
			continueText.text = "Press the keys below...";
			
			

			StartCoroutine(WaitForKeyYoBePressed2());
			

		}
		
		if (_dialoguesCounter == 3)
		{
			buttonJ.SetActive(false);
			buttonK.SetActive(false);
			
			buttonShift.SetActive(true);
			continueText.text = "Press the keys below...";

			StartCoroutine(WaitForKeyYoBePressed3());
			
			

		}
		
	}
	

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) && (_dialoguesCounter == 0 || _dialoguesCounter == 4))
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
		buttonShift.SetActive(false);
		Invoke("TriggerDialogue", 1);
		
		

		if (_dialoguesCounter ==5)
		{
			SceneManager.LoadScene("Level1");
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
				if (Input.GetKeyDown(KeyCode.D))
				{
					keyDpressed = true;
					buttonD.GetComponent<Image>().color = Color.green;
				}
				if (Input.GetKeyDown(KeyCode.Space))
				{
					keySpacepressed = true;
					buttonSpacebar.GetComponent<Image>().color = Color.green;
				}
				
				Debug.Log(keyApressed);
				Debug.Log(keyDpressed);
				Debug.Log(keySpacepressed);
				yield return null;
			}
 
			
			DisplayNextSentence();
			
			//do stuff once space is pressed
 
		}

	IEnumerator WaitForKeyYoBePressed2()
	{

		//do stuff

		{

			bool keyJpressed = false;
			bool keyKpressed = false;




			//do stuff

			//wait for space to be pressed
			while (!keyJpressed || !keyKpressed)
			{
				if (Input.GetKeyDown(KeyCode.J))
				{
					keyJpressed = true;
					buttonJ.GetComponent<Image>().color = Color.green;
				}

				if (Input.GetKeyDown(KeyCode.K))
				{
					keyKpressed = true;
					buttonK.GetComponent<Image>().color = Color.green;
				}


				yield return null;
			}


			DisplayNextSentence();

			//do stuff once space is pressed

		}
	}

	IEnumerator WaitForKeyYoBePressed3()
			{

				//do stuff

				{

					bool keyShiftpressed = false;





					//do stuff

					//wait for space to be pressed
					while (!keyShiftpressed)
					{
						if (Input.GetKeyDown(KeyCode.LeftShift))
						{
							keyShiftpressed = true;
							buttonShift.GetComponent<Image>().color = Color.green;
						}


						yield return null;
					}


					DisplayNextSentence();
					continueText.text = "Press Enter to continue...";
					//do stuff once space is pressed

				}

			}

		
	IEnumerator Wait1Second()
	{
		
		yield return new WaitForSeconds(1f);

	}

}
