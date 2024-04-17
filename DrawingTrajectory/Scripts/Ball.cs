using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Power;
    Rigidbody2D rb;
    LineRenderer line;
    Vector2 DragStartPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        //Creates the trajectory on input
        if(Input.GetMouseButton(0))
        {
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPos - DragStartPosition) * Power;

            Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, 750);
            line.positionCount = trajectory.Length;
            Vector3[] positions = new Vector3[trajectory.Length];
            for(int i = 0; i < trajectory.Length; i++)
            {
                positions[i] = trajectory[i];
            }
            line.SetPositions(positions);
        }

        if(Input.GetMouseButtonUp(0))
        {
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPos - DragStartPosition) * Power;
            rb.velocity = _velocity;

        }
    }

    //handles the creation of the trajectory 
    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity,int step)
    {
        Vector2[] results = new Vector2[step];
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityaccel = Physics2D.gravity * 1 * timestep * timestep;

        float Drag = 1f - timestep * rigidbody.drag;
        Vector2 movestep = velocity * timestep;
        for(int i = 0; i < step; i++)
        {
            movestep += gravityaccel;
            pos += movestep;
            results[i] = pos;
        }
        return results;
    }

    //destroys the ball on collision with other object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
