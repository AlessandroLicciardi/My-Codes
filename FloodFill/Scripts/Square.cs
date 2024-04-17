using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Vector2Int pos;
    MeshRenderer sprite;
    public bool On = false;
    private void Start()
    {
        sprite = GetComponent<MeshRenderer>();

        pos.x = (int)transform.position.x;
        pos.y = (int)transform.position.y;

        if(Random.value < 0.2f)
        {
            SetColorBlack();
            On = (true);
        }
    }

    public void OnMouseOver()
    {
        if(Input.GetMouseButton(0))
        {
            FindObjectOfType<Board>().StartFill(pos);
        }
    }
    public void SetColorBlack()
    {
        sprite.material.color = Color.black;
    }

    public void SetNewColor(Color _color)
    {
        sprite.material.color = _color;
        On = (true);
    }

}
