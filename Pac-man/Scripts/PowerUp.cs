using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Point
{
    public float duration;

    protected override void Collected()
    {
        FindObjectOfType<GameManager>().PowerUpCollected(this);
    }
}
