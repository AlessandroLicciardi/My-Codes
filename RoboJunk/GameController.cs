using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject BGM;
    public GameObject Player/*, GameOverPanel*/;
    float hits, lastscore;
    UiController uiController;
    string scene;
    private Path[] pathList;
    private Item[] ItemList;
    [HideInInspector] public bool isPaused;
    [HideInInspector] public bool isOver;
    public Robot robot;

    public GameObject userPanel;
    public GameObject tutorialPanel;
    public GameObject energyPanel;
    public GameObject scorePanel;
    public GameObject upgradePanel;
    public Text tutorialPopUp;
    public Button tutorialButton;
    public Button tutPlayButton;
    public Button tutMenuButton;
    public string[] tutorialMessages;
    private int currentTutorial = 0;
    private bool IsShooting = false;
    public GameObject RobotDrill;
    private enum eActorState
    {
        Grounded,
        Jumping,
        Falling,
        Dead,
        Digging
    }
    private void Awake()
    {
        
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            GetComponent<GameController>().enabled = false;
        }
        else
        {
            GetComponent<GameController>().enabled = true;
        }
    }

    void Start()
    {
        uiController = FindObjectOfType<UiController>();
        StartCoroutine(uiController.PowerUpBar());
        isPaused = false;
        isOver = false;
        initTutorialMessages();
    }

    void Update()
    {
        CheckTutorial();
        if (Time.timeScale != 0)
        {
            StartCoroutine(uiController.UpdateEnergyBar());
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
            if (uiController.PowerUpSlider.value == uiController.maxPowerUp && robot.shooting != true)
            {
                robot.shooting = true;
                StartCoroutine(Shooting());
            }
            else if (uiController.PowerUpSlider.value == 0)
            {
                StopCoroutine(Shooting());
            }
        }
        CheckDeath();

        if((isPaused || isOver) && SceneManager.GetActiveScene().buildIndex != 2)
        {
            BGM.GetComponent<AudioSource>().Pause();
            robot.GetComponent<Robot>().enabled = false;
        }
        else
        {
            BGM.GetComponent<AudioSource>().UnPause();
            robot.GetComponent<Robot>().enabled = true;
        }

        if(isPaused || isOver)
        {
            RobotDrill.GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            RobotDrill.GetComponent<AudioSource>().enabled = true;
        }

    }

    private void LateUpdate() 
    {
        if (Time.timeScale != 0)
        {
            if(uiController.PowerUpSlider.value == 20)
            { 
                StartCoroutine(Shooting());
            }    
            else if(uiController.PowerUpSlider.value == 0)
            {
                StopCoroutine(Shooting());
            }
        }
    }
    /*
    public void DealDamage(float _damage)
    {
        hits = Mathf.Ceil( uiController.Slider.value ) - _damage; //compute damage
        uiController.Slider.value -= _damage;
        // Debug.Log(hits);
        if (hits <= 0) { uiController.Slider.value = 0;}
        CheckDeath();
    }
    */

    private void CheckDeath()
    {
        if (uiController.Slider.value == 0)
        {
            //deathAudio.Play();
            lastscore = Mathf.Ceil(float.Parse(uiController.ScoreText.text.ToString()));
            if (SetHighScore() < lastscore)  SaveHighScore(lastscore);
            GameOver();
        }
    }


    public float SetHighScore()
    {
        float _lastscore;

        if (PlayerPrefs.HasKey("HighScore"))
        {
            _lastscore = PlayerPrefs.GetFloat("HighScore");
        }
        else
        {
            _lastscore = 0;
        }
        return _lastscore;
    }

    public void SaveHighScore(float _lastscore)
    {
        PlayerPrefs.SetFloat("HighScore", _lastscore);
    }

    
    public void MainMenu()
    {
        float _lastscore;
        _lastscore = SetHighScore();
        if (_lastscore > lastscore)
            SaveHighScore(_lastscore);
    }

    public void CleanUpItem()
    {
        ItemList = FindObjectsOfType<Item>();
        for(int i = 0; i < ItemList.Length; i++)
        {
            GameObject item = ItemList[i].gameObject;
            Destroy(item);
        }
    }

    public void CleanUpTracks()
    {
        pathList = FindObjectsOfType<Path>();
        for(int i = 0; i < pathList.Length; i++)
        {
            GameObject path = pathList[i].gameObject;
            Destroy(path);
        }
    }


    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        uiController.ShowPause(); //game paused message
    }

    void GameOver()
    {
        isOver = true;
        Player.GetComponent<Robot>().state = Robot.eActorState.Dead;
        Time.timeScale = 0;
        userPanel.gameObject.SetActive(false);
        if (uiController.Score > uiController.HighScore)
        {
            uiController.HighScore = uiController.Score;
            uiController.SaveHighScore(uiController.HighScore);
        }
        uiController.ShowGameOver(); //game over message
    }

    IEnumerator Shooting()
    {
        GameObject _proj;  
        while (uiController.PowerUpSlider.value != 0)
        {
            _proj = Instantiate(robot.Shooting_Projectile, robot.transform.position + robot.Shooting_ProjectileSpawnOffset, Quaternion.identity);
            _proj.GetComponent<Projectile>().MoveSpeed = robot.Shooting_projectileSpeed;
            _proj.GetComponent<Projectile>().DespawnOffset = robot.Shooting_ProjectileDespawnOffset;
            yield return new WaitForSeconds(robot.Shooting_delay);
        }
        robot.shooting = false;
    }

    public void TutorialBehavior(int tutorialNumber)
    {
        currentTutorial = tutorialNumber;
        if (currentTutorial != 14 && currentTutorial != 20)
        {
            Time.timeScale = 0;
            tutorialPopUp.text = tutorialMessages[currentTutorial];
            tutorialPanel.SetActive(true);
        }
        switch (tutorialNumber)
        {
            case 6:
                tutorialButton.gameObject.SetActive(false);
                break;
            case 9:
                tutorialButton.gameObject.SetActive(true);
                break;
            case 10:
                tutorialButton.gameObject.SetActive(false);
                break;
            case 15:
                tutorialButton.gameObject.SetActive(true);
                break;
            case 19:
                tutorialButton.gameObject.SetActive(false);
                tutPlayButton.gameObject.SetActive(true);
                tutMenuButton.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        if (tutPlayButton.IsActive())
        {
            tutPlayButton.Select();
        } else
        {
            tutorialButton.Select();
        }
    }

    public void TutorialContinue()
    {
        switch (currentTutorial)
        {
            case 0:
                scorePanel.SetActive(true);
                currentTutorial = 1;
                tutorialPopUp.text = tutorialMessages[currentTutorial];
                tutorialPanel.SetActive(true);
                break;
            case 1:
                isPaused = false;
                Time.timeScale = 1;
                isPaused = false;
                break;
            case 2:
                energyPanel.SetActive(true);
                Time.timeScale = 1;
                isPaused = false;
                break;
            case 3:
                currentTutorial = 4;
                tutorialPopUp.text = tutorialMessages[currentTutorial];
                tutorialPanel.SetActive(true);
                break;
            case 4:
            case 5:
                Time.timeScale = 1;
                isPaused = false;
                break;
            case 6:
                tutorialPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                StartCoroutine(TutorialJump(1.275f));
                break;
            case 7:
                tutorialPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                StartCoroutine(TutorialJump(1.9f));
                break;
            case 8:
                tutorialPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                StartCoroutine(TutorialFastDropAndDig());
                break;
            case 9:
                tutorialPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                break;
            case 10:
                tutorialPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                StartCoroutine(TutorialJump(1.9f));
                break;
            case 11:
                tutorialPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                StartCoroutine(TutorialFastDrop());
                break;
            case 12:
                tutorialPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                StartCoroutine(TutorialJump(1.9f));
                break;
            case 13:
                tutorialPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                StartCoroutine(TutorialFastDropAndDig());
                break;
            case 15:
                upgradePanel.gameObject.SetActive(true);
                tutorialPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                break;
            case 16:
            case 17:
            case 18:
                tutorialPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                break;
            default:
                break;
        }
    }

    private void CheckTutorial()
    {
        if ((currentTutorial == 6 && Input.GetKeyDown(KeyCode.Space))
            || (currentTutorial == 7 && Input.GetKeyDown(KeyCode.Space))
            || (currentTutorial == 8 && Input.GetKeyDown(KeyCode.DownArrow))
            || (currentTutorial == 10 && Input.GetKeyDown(KeyCode.Space))
            || (currentTutorial == 11 && Input.GetKeyDown(KeyCode.DownArrow))
            || (currentTutorial == 12 && Input.GetKeyDown(KeyCode.Space))
            || (currentTutorial == 13 && Input.GetKeyDown(KeyCode.DownArrow))
            )
        {
            isPaused = false;
            TutorialContinue();
        }
        if (currentTutorial == 14)
        {
            Robot Playerscript = Player.GetComponent<Robot>();
            Playerscript.TutorialDig();
            currentTutorial = -1;
        }
    }

    private void initTutorialMessages()
    {
        tutorialMessages = new string[20];
        tutorialMessages[0] = "Ehi there! Welcome to ROBO-JUNK!\n" +
            "Help the robot to keep the planet clean\n" +
            "by collecting junk for as long as you can!";
        tutorialMessages[1] = "As long as the robot keeps running,\n" +
            "your SCORE will constantly increase.\n" +
            "Make sure to keep it alive for as much as you can!";
        tutorialMessages[2] = "Collect as many JUNK as possible\n" +
            "to increase your Score even more!";
        tutorialMessages[3] = "The bar up there shows the robot's ENERGY.\n" +
            "As you keep running, your Energy constantly decreases.\n" +
            "If you run out of Energy, you will LOSE! So, keep an eye on it!";
        tutorialMessages[4] = "To recover some of your Energy,\n" +
            "find and collect BATTERIES along the way!";
        tutorialMessages[5] = "There is an obstacle in front of you!\n" +
            "Let's hit it and see what happens!";
        tutorialMessages[6] = "When you hit an obstacle,\n" +
            "you will lose some of your Energy!\n" +
            "To avoid the next obstacle, PRESS SPACE to JUMP!";
        tutorialMessages[7] = "To avoid an high obstacle,\n" +
            "HOLD SPACE to jump higher!";
        tutorialMessages[8] = "There's also another way to avoid obstacles...\n" +
            "HOLD DOWN to DIG underground!";
        tutorialMessages[9] = "HOLD DOWN to keep running underground, but beware!\n" +
            "Staying underground consumes Energy faster!\n" +
            "By releasing DOWN, the robot will go up again.";
        tutorialMessages[10] = "Want to take all those Junk?\n" +
            "HOLD SPACE to Jump above the obstacle...";
        tutorialMessages[11] = "Now, PRESS DOWN to Drop faster!";
        tutorialMessages[12] = "Want to take the junk again?\n" +
            "Then HOLD SPACE to Jump...";
        tutorialMessages[13] = "Now, PRESS DOWN and HOLD it\n" +
            "to Drop faster and Dig directly underground!";
        tutorialMessages[15] = "Remember to RELEASE DOWN to go up again\n" +
            "and not consume too much Energy!";
        tutorialMessages[16] = "Do you see the new bar up there under the Energy bar?\n" +
            "It's the UPGRADE bar and fills up as you keep collecting Junk.\n" +
            "If it reach the maximum... see what happens!";
        tutorialMessages[17] = "The Upgrade bar starts decreasing again\n" +
            "when you stop picking up Junk!\n" +
            "Make sure to fill the bar up as fastly as you can!";
        tutorialMessages[18] = "When the Upgrade bar is filled, the robot starts SHOOTING,\n" +
            "and you will destroy obstacles that stand in front of you!\n" +
            "It lasts only a few seconds, so make the most of it!";
        tutorialMessages[19] = "That's it! Now you are ready to play!\n" +
            "Help Robo-junk in doing its job and keep the planet clean!";
    }

    IEnumerator TutorialJump(float mod)
    {
        int _currentTutorial = currentTutorial;
        float JumpForce = Player.GetComponent<Robot>().JumpForce;
        float JumpTimeCounter = Player.GetComponent<Robot>().JumpTimeCounter;
        float JumpTime = Player.GetComponent<Robot>().JumpTime;
        Robot Playerscript = Player.GetComponent<Robot>();
        Rigidbody rb = Player.GetComponent<Rigidbody>();
        eActorState state;
        while (_currentTutorial == currentTutorial)
        {
            state =(eActorState) Playerscript.state;
            if (Input.GetKeyDown(KeyCode.Space) && state == eActorState.Grounded && state != eActorState.Dead)
            {
                state = eActorState.Jumping;
                rb.velocity = Vector3.up * JumpForce * mod;
                JumpTimeCounter = JumpTime;
            }

            if (Input.GetKey(KeyCode.Space) && state == eActorState.Jumping)
            {
                if (JumpTimeCounter > 0)
                {
                    rb.velocity = Vector3.up * JumpForce * mod;
                    JumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    state = eActorState.Falling;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) && state != eActorState.Digging)
            {
                state = eActorState.Falling;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator TutorialFastDropAndDig()
    {
        int _currentTutorial = currentTutorial;
        Robot Playerscript = Player.GetComponent<Robot>();
        eActorState state;
        while (_currentTutorial == currentTutorial)
        {
            state = (eActorState)Playerscript.state;
            if (Input.GetKeyDown(KeyCode.DownArrow) /*&& state != eActorState.Grounded*/ && state != eActorState.Digging)
            {
                if (state == eActorState.Grounded && transform.position.y < 1)
                {
                    Playerscript.TutorialDig();
                }
                else
                {
                    Playerscript.TutorialFastDrop();
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator TutorialFastDrop()
    {
        int _currentTutorial = currentTutorial;
        Robot Playerscript = Player.GetComponent<Robot>();
        eActorState state;
        while (_currentTutorial == currentTutorial)
        {
            state = (eActorState)Playerscript.state;
            if (Input.GetKeyDown(KeyCode.DownArrow) /*&& state != eActorState.Grounded*/ && state != eActorState.Digging)
            {
                if (state != eActorState.Grounded || transform.position.y >= 1)
                {
                    Playerscript.TutorialFastDropWithoutDig();
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
