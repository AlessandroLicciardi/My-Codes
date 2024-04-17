using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : EnemyBehaviour
{
    private Node node;
    public GameObject Blinky;
    private void OnDisable() {
        this.enemy.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //starts the chase behaviour depending on the ghost type
        node = other.GetComponent<Node>();
        if(node != null && enemy.GhostType == Enemy.eGhostType.Blinky && !this.enemy.vulnerable.enabled && this.enabled)
        {
            BlinkyChase();
        }
        else if(node != null && enemy.GhostType == Enemy.eGhostType.Pinky && !this.enemy.vulnerable.enabled && this.enabled)
        {
            PinkyChase();
        }
        else if(node != null && enemy.GhostType == Enemy.eGhostType.Inky && !this.enemy.vulnerable.enabled && this.enabled)
        {
            InkyChase();
        }
        else if(node != null && enemy.GhostType == Enemy.eGhostType.Clyde && !this.enemy.vulnerable.enabled && this.enabled)
        {
            ClydeChase();
        }
    }

    public void BlinkyChase()
    {
        GetClosestDirection(enemy.Target.transform.position);
    }

    public void PinkyChase()
    {
        //gets his target 2 nodes ahead of the player
        Vector2 PlayerDirection = enemy.GM.player.Movement.Direction;
        float distance = 0.35f;
        Vector2 _TargetPos = enemy.GM.player.transform.position;
        if(PlayerDirection == Vector2.left)
        {
            _TargetPos.x -= (distance * 2);
        }
        else if(PlayerDirection == Vector2.right)
        {
            _TargetPos.x += (distance * 2);
        }
        else if(PlayerDirection == Vector2.up)
        {
            _TargetPos.y += (distance * 2);
        }
        else if (PlayerDirection == Vector2.down)
        {
            _TargetPos.y -= (distance * 2);
        }

        GetClosestDirection(_TargetPos);
    }

    public void InkyChase()
    {
        //gets his target 2 nodes ahead of pacman and based on the distance of blinky
        Vector2 PlayerDirection = enemy.GM.player.Movement.Direction;
        float distance = 1f;
        Vector2 _TargetPos = enemy.GM.player.transform.position;
        if(PlayerDirection == Vector2.left)
        {
            _TargetPos.x -= (distance * 2);
        }
        else if(PlayerDirection == Vector2.right)
        {
            _TargetPos.x += (distance * 2);
        }
        else if(PlayerDirection == Vector2.up)
        {
            _TargetPos.y += (distance * 2);
        }
        else if (PlayerDirection == Vector2.down)
        {
            _TargetPos.y -= (distance * 2);
        }
        float xDistance = _TargetPos.x - Blinky.transform.position.x;
        float yDistance = _TargetPos.y - Blinky.transform.position.y;

        Vector2 InkyTarget = new Vector2 (_TargetPos.x + xDistance, _TargetPos.y + yDistance);
        GetClosestDirection(InkyTarget);
    }

    public void ClydeChase()
    {
        //same behaviour as blinky
        BlinkyChase();

    }

    private void GetClosestDirection(Vector2 _target)
    {
        //gets the closest distance between the ghost and his target
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;
        foreach(Vector2 availableDirections in node.Directions)
        {
            Vector2 newPos = transform.position + new Vector3(availableDirections.x, availableDirections.y);
            float distance = (_target - newPos).sqrMagnitude;
            if(distance < minDistance)
            {
                direction = availableDirections;
                minDistance = distance;
            }
        }
        //sets the direction of the ghost towards his target
        enemy.movement.SetDirection(direction);
    }

}
