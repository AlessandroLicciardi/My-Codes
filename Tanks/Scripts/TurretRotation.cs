using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public Transform turretPos;

    private void Update()
    {
        var _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotation = Quaternion.LookRotation(_direction - turretPos.transform.position, turretPos.transform.TransformDirection(Vector3.back));
        turretPos.transform.rotation = new Quaternion(0,0, rotation.z, rotation.w);
    }

}
