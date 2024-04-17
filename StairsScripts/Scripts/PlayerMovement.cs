using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    [HideInInspector]
    public Rigidbody rb;
    Movement controls;

    private void OnEnable() {
        controls.Player.Enable();
    }
    private void Awake() {
        controls = new Movement();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Run();
    }

    private void Run()
    {   Vector2 movementInput = controls.Player.Movement.ReadValue<Vector2>();
        Vector3 playerVelocity = new Vector3(movementInput.x * Speed, rb.velocity.y, movementInput.y * Speed);
        rb.velocity = transform.TransformDirection(playerVelocity);
    }
}
