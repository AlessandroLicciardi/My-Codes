using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Damageable
{
    public float MoveSpeed;
    public float rotationSpeed;
    Tank controls;
    Vector2 _direction;
    Rigidbody2D rb;
    UiController ui;
    public AudioSource engineAudio;
    GameController gameController;
    private void OnEnable() 
    {
        controls.Player.Enable();
    }
    private void Awake()
    {
        ui = FindObjectOfType<UiController>();
        rb = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
        controls = new Tank();

    }

    private void Update()
    {
        _direction = controls.Player.Move.ReadValue<Vector2>();
        rb.velocity = new Vector2(_direction.x * MoveSpeed, _direction.y * MoveSpeed);
        
        if(_direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDisable() 
    {
        controls.Player.Disable();
    }

    public override void Hit()
    {
        CurrentHealth--;
        ui.PlayerHUD.UpdateHud(this);
        if(CurrentHealth == 0)
        {
            GetComponentInChildren<TurretRotation>().enabled = false;
            engineAudio.enabled = false;
            ui.ShowDeathScreen();
        }
    }

    public void AddHealth()
    {
        if(CurrentHealth < MaxHealth)
        {
            CurrentHealth++;
            ui.PlayerHUD.UpdateHud(this);
        }
    }
}
