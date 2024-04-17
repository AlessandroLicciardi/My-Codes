using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{   
    public GameObject PauseMenu;
    public void Showpanel(GameObject _dialoguePanel)
    {
        _dialoguePanel.SetActive(true);
    }

    public void Hidepanel(GameObject _dialoguePanel)
    {
        _dialoguePanel.SetActive(false);
    }

    public void ShowPauseMenu(){
        PauseMenu.SetActive(true);
    }

    public void HidePauseMenu(){
        PauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
