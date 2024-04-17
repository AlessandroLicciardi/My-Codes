using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public GameObject Credits;
    public void Play()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        Credits.SetActive(true);
    }

    public void HideCredits()
    {
        Credits.SetActive(false);
    }
}
