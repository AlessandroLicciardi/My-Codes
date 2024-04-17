using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private float ReloadTime = 2f;
    private float time = 0;
    public GameObject BulletPrefab;
    public Transform shootPoint;
    private Collider2D tankCollider2D;
    private Enemy enemy;
    private void Awake()
    {
        tankCollider2D = GetComponent<Collider2D>();
        enemy = GetComponent<Enemy>();
    }
    void Update()
    {
        time += Time.deltaTime;
        if(time >= ReloadTime && enemy.target != null)
        {
            time = 0f;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(BulletPrefab, shootPoint.transform.position, Quaternion.identity);
        newBullet.transform.localRotation = shootPoint.rotation;
        newBullet.GetComponent<Bullet>().Initialize();
        Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(), tankCollider2D);
    }
}
