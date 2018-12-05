using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{

    [HideInInspector]
    public List<string> dialogueSentences = new List<string>();
    [HideInInspector]
    public string npcName;

    public GameObject dialoguePanel;

    Button nextButton, backButton, endButton; //COME BACK AND ADD AN END CONVERSATOIN BUTTON
    Text dialogueText, nameText;
    int dialogueIndex;

    public float typeSpeed;

    #region Singleton/Hookup

    public static DialogueSystem instance { get; set; }

    //This checks the scene to ensure that there
    //Is only one instance of this system in the scene at any given time
    private void Awake()
    {
        nextButton = dialoguePanel.transform.Find("NextButton").GetComponent<Button>();
        backButton = dialoguePanel.transform.Find("BackButton").GetComponent<Button>();
        endButton = dialoguePanel.transform.Find("EndButton").GetComponent<Button>();

        nextButton.onClick.AddListener(delegate { NextDialogue(); } );
        backButton.onClick.AddListener(delegate { PreviousDialogue(); } );
        endButton.onClick.AddListener(delegate { EndDialogue(); });

        dialogueText = dialoguePanel.transform.Find("DialogueText").GetComponent<Text>();
        nameText = dialoguePanel.transform.Find("NPCNamePanel").GetChild(0).GetComponent<Text>(); //is this good enough? There is probably a more efficient way of handeling this

        //COME BACK TO THIS AND HOOK UP ANIMATION
        //WHEN YOUR FINISHED 
        dialoguePanel.SetActive(false);

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
        }
    }

    #endregion

    public void Update()
    {
        //Check to make sure back button is off when beginning dialogue
        if (dialogueIndex <= 0)
        {
            backButton.enabled = false;
            backButton.gameObject.SetActive(false);
        }

        //Check to turn back button on when next dialogue is active
        if (dialogueIndex >= 1)
        {
            backButton.enabled = true;
            backButton.gameObject.SetActive(true);
        }

        //Check to turn on end button and turn off next button
        if (dialogueIndex == dialogueSentences.Count - 1)
        {
            endButton.enabled = true;
            endButton.gameObject.SetActive(true);
            nextButton.enabled = false;
            nextButton.gameObject.SetActive(false);
        }

        else
        {
            endButton.enabled = false;
            endButton.gameObject.SetActive(false);
            nextButton.enabled = true;
            nextButton.gameObject.SetActive(true);
        }
    }

    public void AddNewDialogue(string[] sentences, string npcName)
    {
        dialogueIndex = 0;
        dialogueSentences = new List<string>(sentences.Length);
        dialogueSentences.AddRange(sentences);
        this.npcName = npcName;
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueSentences[dialogueIndex];
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeDialogue(dialogueText.text));
    }

    public void NextDialogue()
    {
        if (dialogueIndex < dialogueSentences.Count - 1)
        {
            StopAllCoroutines();
            dialogueIndex++;
            dialogueText.text = dialogueSentences[dialogueIndex];
            StartCoroutine(TypeDialogue(dialogueText.text));
        }
    }

    public void PreviousDialogue()
    {
        if (dialogueIndex > 0)
        {
            StopAllCoroutines();
            dialogueIndex--;
            dialogueText.text = dialogueSentences[dialogueIndex];
            StartCoroutine(TypeDialogue(dialogueText.text));
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(false);
    }

    IEnumerator TypeDialogue(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

}
