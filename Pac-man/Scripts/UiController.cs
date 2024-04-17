using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiController : MonoBehaviour
{
    public GameManager Gm;
    public GameObject GameOverPanel;
    public GameObject VictoryPanel;
    public Text LivesText;
    public Text HighScoreText;
    public GameObject PausePanel;

    public void SetLivesText(int _lives)
    {
        LivesText.text = _lives.ToString();
    }

    public void SetHighScoreText()
    {
        HighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    
}
