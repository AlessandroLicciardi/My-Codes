using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Score;
    public EnemyScatter scatter;
    public EnemyHome home;
    public Chase chase;
    public EnemyVulnerable vulnerable;
    public GameManager GM;
    public Movement movement;
    public EnemyBehaviour initialState;
    public Transform Target;
    public enum eGhostType
    {
        Blinky,
        Pinky,
        Inky,
        Clyde
    }
    public eGhostType GhostType;
    private void Awake() {
        GM = FindObjectOfType<GameManager>();
        movement = GetComponent<Movement>();
        scatter = GetComponent<EnemyScatter>();
        home = GetComponent<EnemyHome>();
        chase = GetComponent<Chase>();
        vulnerable = GetComponent<EnemyVulnerable>();
    }

    private void Start() {
        ResetEnemy();
    }

    public void ResetEnemy()
    {
        //resets all the ghosts with every behaviour
        movement.ResetDirection();
        
        this.chase.Disable();
        this.scatter.Enable();
        this.vulnerable.Disable();
        if(this.home != initialState)
        {
            this.home.Disable();
        }
        if(this.initialState != null)
        {
            this.initialState.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //checks upon colliding with the player if the ghost is in vulnerable state or not
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(this.vulnerable.enabled)
            {
                GM.EnemyEaten(this);
            }
            else
            {
                GM.PlayerDeath();
            }
        }    
    }
}
