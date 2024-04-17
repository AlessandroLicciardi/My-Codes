using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    [SerializeField] private UiController ui;
    [SerializeField] private GameController gc;
    public IEnumerator DialogueSequence(GameObject _dialoguePanel)
    {
       
        for(int i = 0; i < _dialoguePanel.transform.childCount; i++)
        {
            Deactivate(_dialoguePanel);
            _dialoguePanel.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitUntil(()=> _dialoguePanel.transform.GetChild(i).GetComponent<DialogueLine>().finished);
        }

        ui.Hidepanel(_dialoguePanel);
        gc.EnablePlayer();
        if(gc.Door.canBeOpened == true)
        {
            gc.audiosource.Play();
        }
        else if(gc.nc != null && gc.nc.IsLastNote)
        {
            Application.Quit();
        }
    }

    public void Deactivate(GameObject _dialoguePanel)
    {
        for(int i = 0; i < _dialoguePanel.transform.childCount; i++)
        {
            _dialoguePanel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
