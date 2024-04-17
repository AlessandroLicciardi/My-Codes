using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int score;
    public GameManager GM;

    

    protected virtual void Collected()
    {
        FindObjectOfType<GameManager>().pointCollected(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {    
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Collected();
        }
    }
}
