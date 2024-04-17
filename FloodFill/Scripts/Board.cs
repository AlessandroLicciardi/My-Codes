using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public int xSize, ySize;
    Square[,] squares;
    float lastTick;
    float tickDuration = 0.5f;
    public Color NewColor;
    private void Start()
    {
        lastTick = Time.time;
        squares = new Square[xSize,ySize];
        CreateGrid();

        Camera.main.transform.position = new Vector3(xSize / 2f, ySize / 2f, -((xSize + ySize) / 2f));
        Camera.main.orthographicSize = ySize / 2f;
    }
    public void CreateGrid()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
               GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
               newCube.transform.position = new Vector3(x,y,0);
               squares[x,y] = newCube.AddComponent<Square>();
               squares[x,y].pos = new Vector2Int(x,y);
               
            }
        }
    }
    public void StartFill(Vector2Int pos)
    {
       StartCoroutine (Fill(pos.x, pos.y));
    }

    IEnumerator FillRoutine(int x, int y)
    {
        Fill(x,y);

        yield return null;
    }
    IEnumerator Fill(int x, int y)
    {
        if(!((x < 0 || y < 0) || (x >= xSize || y >= ySize )))
        {
            if(!squares[x,y].On)
            {
                squares[x,y].SetNewColor(NewColor);
                yield return new WaitForSeconds(0.25f);
                StartCoroutine(Fill(x + 1, y));
                StartCoroutine(Fill(x - 1, y));
                StartCoroutine(Fill(x, y + 1));
                StartCoroutine(Fill(x, y - 1));
            }
        }
        yield return new WaitForSeconds(tickDuration);
    }
}
