using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int round = 0;
    public int Enemies = 2;
    public int EnemiesKilled = 0;
    public SpanwerController spanwerController;
    public UiController uiController;
    void Start()
    {
        spanwerController = FindObjectOfType<SpanwerController>();
        uiController = FindObjectOfType<UiController>();
        StartWave();
    }
    
    public void StartWave()
    {
        round = 1;
        Enemies = 2;
        EnemiesKilled = 0;
        spanwerController.SpawnEnemies(Enemies);
        uiController.UpdateRound(round);
        uiController.UpdateEnemyNumber(Enemies);
    }

    public void NextWave()
    {
        round++;
        Enemies += 2;
        EnemiesKilled = 0;
        spanwerController.SpawnEnemies(Enemies);
        uiController.UpdateRound(round);
        uiController.UpdateEnemyNumber(Enemies);
    }

    public void ResetWave()
    {
        round = 1;
        Enemies = 2;
        EnemiesKilled = 0;
        spanwerController.SpawnEnemies(Enemies);
        uiController.UpdateRound(round);
        uiController.UpdateEnemyNumber(Enemies);
    }
}
