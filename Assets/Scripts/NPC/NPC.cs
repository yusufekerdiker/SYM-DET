using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index = 0;

    public float wordSpeed;
    public bool playerIsClose;
    public bool dialogueComplete = false;



    public LevelManager levelManager;
    public GameObject doorObject;


    void Start()
    {
        dialogueText.text = "";
    }

void Update()
{
    if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
    {
        if (!dialoguePanel.activeInHierarchy)
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
        else if (dialogueText.text == dialogue[index])
        {
            NextLine();
            
            if (index == dialogue.Length - 1 && doorObject != null && doorObject.GetComponent<Door>().playerIsClose)
            {
                if (levelManager != null)
                {
                    levelManager.LoadNextLevel();
                }
                else
                {
                    Debug.LogWarning("LevelManager not assigned in NPC script.");
                }
            }
        }
    }
    if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
    {
        RemoveText();
    }
}

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            dialogueComplete = true;
            RemoveText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
        }
    }
}