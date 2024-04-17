using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtons : MonoBehaviour
{
    public UiController ui;

    private void Awake() {
        ui = FindObjectOfType<UiController>();
    }

    public void GameReset()
    {
        ui.Gm.NewGame();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        ui.Gm.ResumeGame();
    }
}
