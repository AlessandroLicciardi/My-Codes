using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float speed;
    public float SpeedVariation;
    public Vector2 initialDirection;
    public Vector2 Direction;
    public Vector2 NextDirection;
    public Vector3 StartingPosition;
    public LayerMask WallLayer;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        StartingPosition = transform.position;
    }

    private void Start()
    {
        ResetDirection();
    }

    public void ResetDirection()
    {
        Direction = initialDirection;
        NextDirection = Vector2.zero;
        transform.position = StartingPosition;
        enabled = true;
    }
    private void Update() 
    {
        //checks every update if we set a NextDirection
        if (NextDirection != Vector2.zero)
        {
            SetDirection(NextDirection);
        }    
    }
    private void FixedUpdate()
    {   
        Vector2 position = rigidBody.position;
        Vector2 translation = Direction * speed * SpeedVariation * Time.fixedDeltaTime;
        rigidBody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 _direction, bool forced = false)
    {
        //set the direction upon input of the player (everything is set in the player script) or
        //if the direction is currently not available, is stored in the NextDirection variable
        if(forced || !CheckTile(_direction))
        {
            Direction = _direction;
            NextDirection = Vector2.zero;
        }
        else
        {
            NextDirection = _direction;
        }
    }

    public bool CheckTile(Vector2 _direction)
    {
        //raycast to check if the direction we want to go to is currently open or is a wall
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.7f, 0.0f, _direction, 1.5f, WallLayer);
        return hit.collider != null; 
    }
}
