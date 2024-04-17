using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    private float duration = 0;
    private float LifeTime = 3f;
    Rigidbody2D rb;
    Damageable _dmg;
    public GameObject ExplosionPrefab;
    public AudioClip ExplosionSound;
    GameController gameController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
    }
    private void Update()
    {
        duration += Time.deltaTime;
        if(duration >= LifeTime)
        {
            Destroy();
        }
        Physics2D.IgnoreLayerCollision(3,3);
    }

    public void Initialize()
    {
        rb.velocity = transform.up * speed;
    }
    private void Destroy()
    {
        rb.velocity = Vector2.zero;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        var damagable = collision.GetComponent<Damageable>();
        if(damagable != null)
        {
            damagable.Hit();
        }
        Destroy();
        gameController.audioController.PlayClip(ExplosionSound, 0.1f);
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }

}
