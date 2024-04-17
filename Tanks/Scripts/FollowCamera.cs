using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Player Player;

    private void Start()
    {
        Player = FindObjectOfType<Player>();
    }
    private void Update() 
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -2);
    }
}
