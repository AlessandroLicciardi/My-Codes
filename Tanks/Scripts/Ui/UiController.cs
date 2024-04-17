using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public GameObject VictoryScreen;
    public HealthBar PlayerHUD;
    public GameObject DeathPanel;
    public GameObject PausePanel;
    public Text Round;
    public Text EnemyNumber;
    GameController gameController;
    public GameObject cupcake;
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    public void UpdateRound(int round)
    {
        Round.text = round.ToString();
    }
    public void UpdateEnemyNumber(int _number)
    {
        EnemyNumber.text = _number.ToString();
    }
    public void ShowDeathScreen()
    {
        Time.timeScale = 0;
        DeathPanel.SetActive(true);
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
    }    
    public void UnPause()
    {
        PausePanel.SetActive(false);
        gameController.player.engineAudio.enabled = true;
        Time.timeScale = 1;
        FindObjectOfType<TurretRotation>().GetComponent<TurretRotation>().enabled = true;
        gameController.BGM.SetActive(true);
    
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ShowVictoryScreen()
    {
        VictoryScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowCupcake()
    {
        cupcake.SetActive(true);
    }
}
