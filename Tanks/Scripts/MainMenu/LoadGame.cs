using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    private void Start()
    {
        LoadingGame();
        Time.timeScale = 1;
    }

    private void LoadingGame()
    {
        StartCoroutine(LoadSceneAsync(2));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        yield return new WaitForSeconds(2f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        yield return null;
    }
}
