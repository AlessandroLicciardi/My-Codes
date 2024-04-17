using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameStarter : MonoBehaviour
{   
    [SerializeField] private GameObject options;
    public Slider slider;
    public float sliderValue;
    private void Start() {
        slider.value = PlayerPrefs.GetFloat("Sens");
    }
    public void Play()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    public void OpenOptions()
    {
        options.SetActive(true);
    }

    public void CloseOptions()
    {
        options.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void ChangeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("Sens", sliderValue);
    }
}
