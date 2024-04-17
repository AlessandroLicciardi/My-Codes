using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportPosition;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Vector3 Pos = other.transform.position;
        Pos.x = teleportPosition.transform.position.x;
        Pos.y = teleportPosition.transform.position.y;
        other.transform.position = Pos;
    }
}
