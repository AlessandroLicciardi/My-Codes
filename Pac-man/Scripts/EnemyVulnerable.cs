using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVulnerable : EnemyBehaviour
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer vulnerable;
    public SpriteRenderer vulnerableEnding;

    public bool eaten;

    public override void Enable(float duration)
    {
        base.Enable(duration);

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.vulnerable.enabled = true;
        this.vulnerableEnding.enabled = false;

        Invoke(nameof(Ending), duration / 2f);
    }

    public override void Disable()
    {
        base.Disable();

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.vulnerable.enabled = false;
        this.vulnerableEnding.enabled = false;
    }

    //will change the sprite of the ghost to blink from blue to white when the duration of the vulnerable state is edning
    private void Ending()
    {
        if(!this.eaten)
        {
            this.vulnerable.enabled = false;
            this.vulnerableEnding.enabled = true;
            this.vulnerableEnding.GetComponent<AnimatedSprite>().RestartAnimation();
        }

    }

    private void Eaten()
    {
        eaten = true;

        Vector3 pos = this.enemy.home.firstPos.position;
        pos.z = this.enemy.transform.position.z;
        this.enemy.transform.position = pos;
        
        this.enemy.home.Enable(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.vulnerable.enabled = false;
        this.vulnerableEnding.enabled = false;
    }
    private void OnEnable() {
        this.enemy.movement.SpeedVariation = 0.5f;
        this.eaten = false;
    }

    private void OnDisable() {
        this.enemy.movement.SpeedVariation = 1f;
        this.eaten = false;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(this.enabled)
            {
                Eaten();
            }
        }    
    }
    //stays away from his target (the player)
    private void OnTriggerEnter2D(Collider2D other) 
    { 
        Node node = other.GetComponent<Node>();
        if(node != null && this.enabled)
        {
            Vector2 _direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach(Vector2 direction in node.Directions)
            {
                Vector3 newPos = this.transform.position + new Vector3(direction.x, direction.y, 0.0f);
                float distance = (this.enemy.Target.position - newPos).sqrMagnitude;

                if(distance > maxDistance)
                {
                    _direction = direction;
                    maxDistance = distance;
                }
            }
            this.enemy.movement.SetDirection(_direction);
        }
    }
}
