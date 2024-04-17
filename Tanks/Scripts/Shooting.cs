using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject BulletPrefab;
    private float ReloadTime = 0f;
    private float timeToWait = 2f;
    private Collider2D tankCollider2D;

    private void Awake()
    {
        tankCollider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        ReloadTime += Time.deltaTime;
        if(Input.GetMouseButton(0))
        {
            if(ReloadTime >= timeToWait)
            {
                ReloadTime = 0f;
                Shoot();
            }
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
