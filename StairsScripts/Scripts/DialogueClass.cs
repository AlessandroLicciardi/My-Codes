using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueClass : MonoBehaviour
{
    public bool finished;
    [SerializeField] private GameController gc;
    protected IEnumerator WriteText(string input, Text textHolder)
    {
        for(int i = 0; i< input.Length; i++)
        {
            textHolder.text += input[i];
            yield return new WaitForSeconds(0.15f);
        }   

        yield return new WaitUntil(() => Input.GetMouseButton(0));
        finished = true;
    }
}
