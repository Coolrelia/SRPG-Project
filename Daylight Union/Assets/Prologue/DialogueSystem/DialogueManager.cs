using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public bool sentenceEnded = false;

    public Text nameText;
    public Text dialogueText;

    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;
    public Image image6;

    public GameObject letter;
    public GameObject alarm;

    private Queue<string> sentences;

    public static int currentScene = 5;
    public int currentDialogue = 1;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentenceEnded = false;
        nameText.text = dialogue.name;


        switch (currentScene)//Checks which scene it is, 
        {
            case 1://Scene 1
                switch (currentDialogue)//Checks which dialogue number it is, so that it can display the appropriate character
                {
                    case 1:
                        image2.gameObject.SetActive(true);
                        break;
                    case 2:
                        image3.gameObject.SetActive(true);
                        break;
                    case 3:
                        image4.gameObject.SetActive(true);
                        break;
                }
                break;

            case 2://Scene 2
                switch (currentDialogue)
                {
                    case 2:
                        image2.gameObject.SetActive(true);
                        break;
                    case 3:
                        image3.gameObject.SetActive(true);
                        break;
                    case 4:
                        image4.gameObject.SetActive(true);
                        image2.gameObject.SetActive(false);
                        break;
                    case 5:
                        image5.gameObject.SetActive(true);
                        break;
                    case 6:
                        image3.gameObject.SetActive(true);
                        break;
                    case 8:
                        letter.SetActive(true);
                        break;
                    case 9:
                        letter.SetActive(false);
                        break;
                    case 10:
                        image4.gameObject.SetActive(false);
                        image2.gameObject.SetActive(true);
                        break;
                    case 21:
                        image5.gameObject.SetActive(false);
                        break;
                    case 24:
                        image1.gameObject.SetActive(false);
                        break;
                    case 26:
                        image2.gameObject.SetActive(false);
                        image4.gameObject.SetActive(true);
                        break;
                }
                break;

            case 3:
                switch(currentDialogue)
                {
                    case 1:
                        alarm.SetActive(true);
                        break;
                    case 2:
                        image2.gameObject.SetActive(true);
                        break;
                }
                break;

            case 4:
                switch(currentDialogue)
                {
                    case 2:
                        image2.gameObject.SetActive(true);
                        break;
                }
                break;

            case 5:
                switch(currentDialogue)
                {
                    case 2:
                        image2.gameObject.SetActive(true);
                        break;
                    case 3:
                        image3.gameObject.SetActive(true);
                        break;
                    case 4:
                        image4.gameObject.SetActive(true);
                        break;
                    case 5:
                        image3.gameObject.SetActive(false);
                        image5.gameObject.SetActive(true);
                        break;
                    case 6:
                        image5.gameObject.SetActive(false);
                        image6.gameObject.SetActive(true);
                        break;
                }
                break;
        }

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        currentDialogue++;
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
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
        sentenceEnded = true;
    }
}
