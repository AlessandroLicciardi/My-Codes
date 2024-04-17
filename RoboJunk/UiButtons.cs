using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiButtons : MonoBehaviour
{
    public UiController uiController;
    public GameController gameController;
    public PathController path;
    public Robot robot;

    public void Retry()
    {
        gameController.userPanel.SetActive(true);
        gameController.CleanUpItem();
        gameController.CleanUpTracks();
        uiController.HideGameOver();
        uiController.Score = 0;
        uiController.totalDistance = 0;
        uiController.totalJunk = 0;
        uiController.UpdateScore(0);
        uiController.PowerUpSlider.value = 0;
        uiController.currentEnergy = uiController.MaxEnergy;
        uiController.SetEnergyBar();
        robot.transform.position = new Vector3(-1.9f, 0, 0);
        Time.timeScale = 1;
        path.FillTracks();
        robot.state = Robot.eActorState.Grounded;
        robot.ground.GetComponent<MeshCollider>().enabled = true;
        robot.RobotNormal.SetActive(true);
        robot.RobotDrill.SetActive(false);
        robot.RobotDrill.GetComponent<AudioSource>().enabled = true;
        gameController.GetComponent<PathController>().resetCounter();
        gameController.isOver = false;
        //robot.RetryReset();
    }

    public void Resume()
    {
        uiController.HidePause();
        Time.timeScale = 1;
        gameController.isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueTutorial()
    {
        gameController.tutorialPanel.SetActive(false);
        gameController.TutorialContinue();
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        Retry();
        uiController.Slider.gameObject.SetActive(false);
        uiController.PowerUpSlider.gameObject.SetActive(false);
        uiController.ScoreLabelText.gameObject.SetActive(false);
        uiController.ScoreText.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void MenuTutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
