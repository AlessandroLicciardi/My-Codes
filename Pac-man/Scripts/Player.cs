using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Movement Movement;
    public GameManager gm;

    private void Awake() {
        Movement = GetComponent<Movement>();
        ResetPlayer();
    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Movement.SetDirection(Vector2.up);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Movement.SetDirection(Vector2.right);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Movement.SetDirection(Vector2.left);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Movement.SetDirection(Vector2.down);
        }

        float angle = Mathf.Atan2(Movement.Direction.y, Movement.Direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gm.PauseGame();
        }
    }

    public void ResetPlayer()
    {
        Movement.ResetDirection();
    }
}
