using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject ballPrefab;
    public Transform ShotPoint;
    GameObject newBall;

    private void Update()
    {
        //handles VerticalRotation of the Cannon and instantiate the ball to shoot
        float VerticalRotation = Input.GetAxis("Vertical");
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0,0, VerticalRotation * rotationSpeed));

        if(Input.GetMouseButtonDown(0))
        {
            newBall = Instantiate(ballPrefab, ShotPoint.transform.position, Quaternion.identity);
            newBall.GetComponent<Rigidbody2D>().gravityScale = 0; 
            
        }

        if(Input.GetMouseButtonUp(0))
        {
            newBall.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
