using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public DoorController Door;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private CameraMovement cm;
    [SerializeField] private DialogueHolder dh;
    [SerializeField] public UiController ui;
    [SerializeField] public GameObject dialoguePanel;
    [SerializeField] public GameObject KeyDialogue;    
    public NoteController nc;
    public Slider slider;
    public float sliderValue;
    [SerializeField] private GameObject writing;
    [SerializeField] public AudioSource audiosource;
    [SerializeField] private GameObject EndingPanel;

    public void OpenDoor()
    {
        DisablePlayer();
        Door.canBeOpened = true;
        writing.SetActive(true);
        dh.StartCoroutine(dh.DialogueSequence(KeyDialogue));
    }
     private void Start() {
        slider.value = PlayerPrefs.GetFloat("Sens");
    }

    private void Awake() {
        DisablePlayer();
        dh.StartCoroutine(dh.DialogueSequence(dialoguePanel));
    }

    public void DisablePlayer()
    {
        Cursor.lockState = CursorLockMode.None;
        cm.enabled = false;
        pm.enabled = false;
    }

    public void EnablePlayer()
    {   
        Cursor.lockState = CursorLockMode.Locked;
        cm.enabled = true;
        pm.enabled = true;
    }

    public void CloseNote()
    {
        nc.HidePanel();
        nc.ShowDialogue();
        if(nc.IsLastNote)
        {
            EndingPanel.SetActive(true);
            EndGame();
        }
    }

    public void HidePause()
    {
        ui.HidePauseMenu();
        EnablePlayer();
        cm.Sensitivity = PlayerPrefs.GetFloat("Sens");
    }
    public void ChangeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("Sens", sliderValue);
    }

    public void EndGame()
    {
        Door.PlaySound();

    }
}
