using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueDialogue : MonoBehaviour
{
    public DialogueManager dm;

    public GameObject startDialogue;

    public List<GameObject> dialogue = new List<GameObject>();
    public GameObject activeDialogue;

    public int dialogueNumber = 1;

    void Start()
    {
        startDialogue.GetComponent<DialogueTrigger>().TriggerDialogue();

        GameObject[] allDialogue = GameObject.FindGameObjectsWithTag("Dialogue");
        dialogue.AddRange(allDialogue);

        activeDialogue = dialogue[dialogueNumber];
    }

    void Update()
    {
        if(dialogue.Count > 0)
        {
            activeDialogue = dialogue[0];
        }

        if (dm.sentenceEnded == true)
        {
            activeDialogue.GetComponent<DialogueTrigger>().TriggerDialogue();
            dialogue.RemoveAt(0);    
        }
    }

    public void NextScene()
    {
        if(dialogue.Count == 0)
        {
            DialogueManager.currentScene++;
            SceneManager.LoadScene("Cutscene" + (DialogueManager.currentScene).ToString());
        }
    }
}
