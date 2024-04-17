using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] public bool canBeOpened;
    private AudioSource audioSource;
    [SerializeField] public GameObject dialoguePanel;
    [SerializeField] private DialogueHolder dh;
    [SerializeField] private GameController gc;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Open()
    {
        if(canBeOpened)
        {
            anim.SetTrigger("Opened");
        }
        else
        {
            gc.DisablePlayer();
            dh.StartCoroutine(dh.DialogueSequence(dialoguePanel));
        }
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
