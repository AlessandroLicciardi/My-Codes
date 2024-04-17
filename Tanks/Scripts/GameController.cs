using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioController audioController;
    public GameObject BGM;
    //public GameObject VictoryMusic;
    public WaveManager wave;
    public Player player;
    UiController Ui;

    private void Start()
    {
        wave = GetComponentInChildren<WaveManager>();
        Ui = FindObjectOfType<UiController>();
        player = FindObjectOfType<Player>();
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(wave.EnemiesKilled >= wave.Enemies)
        {
            if(wave.round == 10)
            {
                Ui.ShowVictoryScreen();
                BGM.SetActive(false);
                //VictoryMusic.SetActive(true);
            }
            else
            {
                wave.NextWave();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {   
            BGM.SetActive(false);
            player.engineAudio.enabled = false;
            Ui.Pause();
            FindObjectOfType<TurretRotation>().GetComponent<TurretRotation>().enabled = false;
            Time.timeScale = 0;
        }
    }
}
