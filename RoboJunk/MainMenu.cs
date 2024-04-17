using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public Button firstButton;
    public Text highScore;

    private void Start()
    {
        Cursor.visible = false;
        if (highScore != null)
        {
            highScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
        }
    }

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(firstButton.gameObject);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
