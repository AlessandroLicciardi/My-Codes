using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    public GameObject MedKitPrefab;
    private float chance = 0.2f;
    WaveManager wave;
    UiController Ui;
    void Start()
    {
        wave = FindObjectOfType<GameController>().GetComponentInChildren<WaveManager>();
        Ui = FindObjectOfType<UiController>();
        CurrentHealth = MaxHealth;
    }

    public virtual void Hit()
    {
        CurrentHealth--;
        if(CurrentHealth == 0)
        {
            wave.EnemiesKilled++;
            int num = wave.Enemies - wave.EnemiesKilled;
            Ui.UpdateEnemyNumber(num);
            Destroy(gameObject);
            if(Random.value < chance)
            {
                chance = 0.2f;
                Instantiate(MedKitPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                chance += 0.1f;
            }
        }
    }

    
}
