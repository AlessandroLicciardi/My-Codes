using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject[] dots;
    private void Awake()
    {
        StartCoroutine(LoadGame());
        StartCoroutine(LoadingDots());
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainGame");
    }

    private IEnumerator LoadingDots()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.8f);
            dots[i].SetActive(true);
        }
    }
}
