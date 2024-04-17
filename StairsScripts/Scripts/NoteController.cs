using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NoteController : MonoBehaviour
{
    public GameObject Panel;
    [SerializeField] private GameController gc;
    [SerializeField] private TMP_Text text;
    [Space(15)]
    [SerializeField] [TextArea] private string noteText;
    [SerializeField] private DialogueHolder dh;
    [SerializeField] private GameObject dialoguePanel;
    public bool IsLastNote;
    public void ShowPanel()
    {
        text.text = noteText;
        Panel.SetActive(true);
    }

    public void HidePanel()
    {   
        //ShowDialogue();
        Panel.SetActive(false);
    }

    public void ShowDialogue()
    {
        dh.StartCoroutine(dh.DialogueSequence(dialoguePanel));
    }
    
}
