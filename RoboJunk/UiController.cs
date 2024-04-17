using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EZCameraShake;

public class UiController : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public GameObject UiPanel;    
    public Slider PowerUpSlider;
    public GameObject Player;
    public Robot robot;

    public float HighScore;
    public Text HighscoreText;
    public Text DistanceScoreText;
    public Text JunkScoreText;
    public Text TotalScoreText;
    [SerializeField] [Range(0f, 0.1f)] float BatteryLoad;
    [SerializeField] [Range(0.1f, 2f)] float EnergyBarRefreshTime;
    [SerializeField] [Range(0.1f, 100f)] public float MaxEnergy;

    public Slider Slider;
    public float currentEnergy;
    public Text ScoreLabelText;
    public Text ScoreText;
    public int Score;
    public int ScoreDistance;
    public float ScoreDistanceTime;
    public int totalDistance;
    public int totalJunk;
    public float maxPowerUp;
    private float currentBatteryLoad;

    [Header("Power Up Bar")]
    [SerializeField] [Range(0f, 0.1f)] public float load;
    [SerializeField] [Range(0f, 0.1f)] public float ReloadBarTime;

    private void Awake()
    {
        GameOverPanel.SetActive(false);
        HighScore = SetHighScore();
        currentEnergy = MaxEnergy;
        PowerUpSlider.maxValue = maxPowerUp;
        PowerUpSlider.value = 0;
        currentBatteryLoad = BatteryLoad;
    }

    private void Start()
    {
        Score = 0;
        totalDistance = 0;
        totalJunk = 0;
        UpdateScore(Score);
        UiPanel.SetActive(false);
        HidePause();
        HideGameOver();
        StartCoroutine(Distance());
        Slider.maxValue = MaxEnergy;

        /*
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Slider.gameObject.SetActive(false);
            PowerUpSlider.gameObject.SetActive(false);
            ScoreText.gameObject.SetActive(false);
        }
        */
    }
    public void SetEnergyBar()
    {
        if (Player != null)
        {
            Slider.value = currentEnergy;
        }
    }

    public IEnumerator UpdateEnergyBar()
    {
        yield return new WaitForSeconds(EnergyBarRefreshTime);
        if(robot.state == Robot.eActorState.Digging)
        {
            currentEnergy -= (currentBatteryLoad * 2f);
            SetEnergyBar();
        }
        else
        {
            currentEnergy -= currentBatteryLoad;
            SetEnergyBar();
        }
    }

    public void CheckBar()
    {
        if(Slider.value <= 30)
        {
            currentBatteryLoad = BatteryLoad/2;
        }
        else if(Slider.value > 30)
        {
            currentBatteryLoad = BatteryLoad;
        }
    }



    public void UpdateScore(int _score)
    {
        if (Player != null)
        { 
            if (_score > 5)
            {
                totalJunk += 10;
                // robot.Shooting_AddTrashCounter();
                // if(!robot.shooting)
                // {
                //     PowerUpSlider.value = robot.ShootingCurrentTrash;
                // }
            }
            Score += _score;
            ScoreText.text = Score.ToString();
        }
    }

    IEnumerator Distance()
    {
        totalDistance += ScoreDistance;
        Score += ScoreDistance;
        ScoreText.text = Score.ToString();
        yield return new WaitForSeconds(ScoreDistanceTime);
        StartCoroutine(Distance());
    }



    public void HidePause()
    {
       PausePanel.SetActive(false);
       UiPanel.SetActive(false);
    }
    public void ShowPause()
    {
       PausePanel.SetActive(true);
       UiPanel.SetActive(true);
    }

    public void HideGameOver()
    {
       GameOverPanel.SetActive(false);
       UiPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        GameOverPanel.SetActive(true);
        DistanceScoreText.text = totalDistance.ToString();
        JunkScoreText.text = totalJunk.ToString();
        TotalScoreText.text = Score.ToString();
        HighscoreText.text = HighScore.ToString();
        UiPanel.SetActive(true);
    }

    public float SetHighScore()
    {
        float _lastscore;

        if (PlayerPrefs.HasKey("High Score"))
        {
            _lastscore = PlayerPrefs.GetFloat("High Score");
        }
        else
        {
            _lastscore = 0;
        }
        return _lastscore;
    }

    public void SaveHighScore(float HighScore)
    {
        PlayerPrefs.SetFloat("High Score", HighScore);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("High Score");
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
    }
    public void AddEnergy(float _value)
    {
        if (Player != null)
        {
            currentEnergy += _value;
            if (currentEnergy >= MaxEnergy)
            {
                currentEnergy = MaxEnergy;
            }
            Slider.value = currentEnergy;
            //Debug.Log(Slider.value);
        }
    }
    public void DealDamage(float _value)
    {
        if (Player != null)
        {
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            currentEnergy -= _value;
            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
            }
            Slider.value = currentEnergy;
            //Debug.Log("damage: " + Slider.value);
        }
    }

    public IEnumerator PowerUpBar()
    {
        yield return new WaitForSeconds(ReloadBarTime);
        if(Time.timeScale == 1)
        {
            if(!robot.shooting)
            {
                PowerUpSlider.value -= load;
            }
            else
            {
                PowerUpSlider.value -= load * 6;
            }
        
        }
        StartCoroutine(PowerUpBar());
    }

    public void UpdatePowerUpBar(float _value)
    {   
        PowerUpSlider.value += _value;
    }
}
