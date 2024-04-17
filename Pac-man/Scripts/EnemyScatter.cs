using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScatter : EnemyBehaviour
{
    private Node node;
    private void OnDisable() {
        this.enemy.chase.Enable();
    }
    //upon colliding with a node, picks a random available direction
    //the next available direction cant be the direction the ghost was coming from
    private void OnTriggerEnter2D(Collider2D other) 
    { 
        node = other.GetComponent<Node>();
        if(node != null && this.enabled && !this.enemy.vulnerable.enabled)
        {
            int index = Random.Range(0, node.Directions.Count);
            
            if(node.Directions[index] == -enemy.movement.Direction && node.Directions.Count > 1)
            {
                index++;
                if(index >= node.Directions.Count)
                {
                    index = 0;
                }
            }
            enemy.movement.SetDirection(node.Directions[index]); 
        }
    }
}
