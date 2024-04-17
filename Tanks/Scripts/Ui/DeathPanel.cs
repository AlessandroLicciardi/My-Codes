using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathPanel : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
