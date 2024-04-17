using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    public SpriteRenderer eyes;
    public Movement movement;

    //simple script that changes the direction of eyes
    private void Awake() {
        this.eyes = GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<Movement>();
    }

    private void Update() {
        if(this.movement.Direction == Vector2.up)
        {
            this.eyes.sprite = this.up;
        }
        else if(this.movement.Direction == Vector2.down)
        {
            this.eyes.sprite = this.down;
        }
        else if(this.movement.Direction == Vector2.left)
        {
            this.eyes.sprite = this.left;
        }
        else if(this.movement.Direction == Vector2.right)
        {
            this.eyes.sprite = this.right;
        }
    }
}
