using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentenses;
    public Text nameText;
    public Text dialogueText;
    public GameObject DialogueSystem;
    [SerializeField] private AudioClip Dialog;
    public weapon Weapon;


    void Start()
    {
        sentenses = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        Weapon.enabled = false;
        nameText.text = dialogue.name;

        sentenses.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentenses.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    
    public void DisplayNextSentence()
    {
        if (sentenses.Count == 0 )
        {
            EndDialogue();
            return;
        }
        string sentence = sentenses.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())

        {
            dialogueText.text += letter;
            yield return null;
        }

        SoundManager.instance.PlaySound(Dialog);
    }

    void EndDialogue()
    {
        if (DialogueSystem != null)
        {
            Weapon.enabled = true;
            bool isActive = DialogueSystem.activeSelf;

            DialogueSystem.SetActive(!isActive);
        }
    }


}
