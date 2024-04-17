using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueLine : DialogueClass
{
    [SerializeField] private string input;
    [SerializeField] private UiController ui;
    [SerializeField] private Text textHolder;

    
    private void Awake() {
        StartDialogue();
    }

    public void StartDialogue() {
        StartCoroutine(WriteText(input, textHolder));
    }
}
