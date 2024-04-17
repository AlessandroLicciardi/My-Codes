using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource[] BGMsources;
    public Enemy[] enemy;
    public Player player;
    public Transform points;
    public int Multiplier;
    public int score;
    public int lives;
    public Text scoreText;
    public UiController ui;
    public AudioClip[] pointEaten;
    private int pointSoundIndex = 0;
    public AudioClip deathSound;
    public AudioManager audioManager;
    public AudioClip GhostEaten;

    private void Awake()
    {
        BGMsources = Camera.main.GetComponents<AudioSource>();
    }
    public void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        SetLives(3);
        ResetGame();
        ui.SetHighScoreText();
        ui.GameOverPanel.SetActive(false);
        ui.VictoryPanel.SetActive(false);
        ui.PausePanel.SetActive(false);
    }
    private void ResetGame()
    {
        //resets the game
        foreach (Transform point in points)
        {
            point.gameObject.SetActive(true);
        }
        SetScore(0);
        SetScoreText();
        SetLives(3);
        ui.SetLivesText(lives);
        Reset();
    }
    public void SetScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void pointCollected(Point _point)
    {
        //upon collecting a point increases the score and then checks if every point has been collected
        _point.gameObject.SetActive(false);
        SetScore(score + _point.score);
        SetScoreText();
        CheckNewHighScore();
        ui.SetHighScoreText();
        PlayPointSound();
        if(!RemainingPoints())
        {
            //if every point has been collected the game ends with the player's victory
            player.gameObject.SetActive(false);
            for(int i = 0; i < enemy.Length; i++)
            {
                this.enemy[i].gameObject.SetActive(false);
            }
            ui.VictoryPanel.SetActive(true);
            BGMsources[0].enabled = false;
            BGMsources[1].enabled = false;
            Time.timeScale = 0;

        }
    }

    public void PlayDeathSound()
    {
        audioManager.PlayClip(deathSound);
    }
    public void PlayPointSound()
    {
        audioManager.PlayClip(pointEaten[pointSoundIndex]);
        pointSoundIndex++;
        if(pointSoundIndex == 1)
        {
            pointSoundIndex = 0;
        }
    }

    public void PowerUpCollected(PowerUp _powerUp)
    {
        for(int i = 0; i < enemy.Length; i++)
        {
            this.enemy[i].vulnerable.Enable(_powerUp.duration);
        }
        BGMsources[0].enabled = false;
        BGMsources[1].enabled = true;
        pointCollected(_powerUp);
        CancelInvoke();
        Invoke(nameof(ResetMultiplier), _powerUp.duration);
        
    }

    private void Reset()
    {
        Time.timeScale = 1;
        BGMsources[0].enabled = true;
        BGMsources[1].enabled = false;
        pointSoundIndex = 0;
        ResetMultiplier();
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].gameObject.SetActive(true);
            enemy[i].ResetEnemy();
        }
        
        player.gameObject.SetActive(true);
        player.ResetPlayer();
    }

    private void SetScore(int _score)
    {
        score = _score;
    }

    private void SetLives(int _lives)
    {
        lives = _lives;
    }

    private void GameOver()
    {
        ui.GameOverPanel.SetActive(true);
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].gameObject.SetActive(false);
            enemy[i].ResetEnemy();
        }
        BGMsources[0].enabled = false;
        BGMsources[1].enabled = false;
        player.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void EnemyEaten(Enemy enemy)
    {
        //updates the score upon eating a ghost
        SetScore(this.score + (enemy.Score * Multiplier));
        audioManager.PlayClip(GhostEaten);
        SetScoreText();
        Multiplier++;
    }

    public void PlayerDeath()
    {
        player.gameObject.SetActive(false);
        PlayDeathSound();
        SetLives(lives - 1);
        if(lives > 0)
        {
            Invoke(nameof(Reset), 2.0f);
            ui.SetLivesText(lives);
        }
        else
        {
            ui.SetLivesText(lives);
            GameOver();
        }

    }

    private bool RemainingPoints()
    {
        foreach(Transform point in points)
        {  
            if(point.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetMultiplier()
    {
        //reset the multiplier of when you eat multiple ghosts
        BGMsources[0].enabled = true;
        BGMsources[1].enabled = false;
        Multiplier = 1;
    }

    public void CheckNewHighScore()
    {
        if(score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void ResumeGame()
    {
        ui.PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        ui.PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
