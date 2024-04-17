using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwerController : MonoBehaviour
{
    public GameObject[] Spawners;
    public GameObject EnemyTankPrefab;
    public void SpawnEnemies(int EnemyNumber)
    {
        StartCoroutine(Spawn(EnemyNumber));
    }

    IEnumerator Spawn(int _number)
    {
        for(int i = 0; i < _number; i++)
        {   
            Instantiate(EnemyTankPrefab, Spawners[Random.Range(0,5)].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
}
