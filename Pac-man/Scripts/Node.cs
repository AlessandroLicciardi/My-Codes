using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> Directions;
    public LayerMask WallLayer;

    private void Awake() 
    {   
        this.Directions = new List<Vector2>();
    }
    //script that allow us to check all the available direction every node has
    //a node was placed in every intersection
    private void Start() 
    {
        CheckDirection(Vector2.up);
        CheckDirection(Vector2.down);
        CheckDirection(Vector2.left);
        CheckDirection(Vector2.right);
    }
    private void CheckDirection(Vector2 _direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, _direction, 1.5f, WallLayer);
        if(hit.collider == null)
        {
            this.Directions.Add(_direction);
        }
    }
}
