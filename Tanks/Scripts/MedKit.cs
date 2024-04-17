using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    BoxCollider2D Collider;
    GameController gameController;
    public AudioClip HealthUp;
    private void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        gameController = FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter2D(Collider2D Collider) 
    {
        if(Collider.tag == "Player")
        {
            Collider.GetComponent<Player>().AddHealth();
            gameController.audioController.PlayClip(HealthUp, 0.6f);
            Destroy(gameObject);
        }
    }
}
