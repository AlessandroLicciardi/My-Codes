using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public Transform turret;
    public Player _player;
    private void Start() 
    {
        _player = FindObjectOfType<Player>();    
    }
    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(_player.transform.position - turret.transform.position, turret.TransformDirection(Vector3.back));
        turret.transform.rotation = new Quaternion(0,0, rotation.z, rotation.w);
    }
}
