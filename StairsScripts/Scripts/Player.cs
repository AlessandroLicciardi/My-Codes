using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int RayLenght = 5;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layer;
    private NoteController note;
    private DoorController door;
    [SerializeField] private GameObject key;
    [SerializeField] private AudioSource Walking;
    [SerializeField] private GameController gc;
    private PlayerMovement movement;
    [HideInInspector]
    public KeyCode InteractKey = KeyCode.E;

    private void Awake() {
        movement = GetComponent<PlayerMovement>();
    }
    
    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position, fwd, out hit, RayLenght, layerMask))
        {
            if(hit.collider.CompareTag("Door"))
            {   
                door = hit.collider.gameObject.GetComponentInParent<DoorController>();
                if(Input.GetKeyDown(InteractKey))
                {
                    door.Open();
                }
            }
            
            if(hit.collider.CompareTag("Key"))
            {
                key = hit.collider.gameObject;
                if(Input.GetKeyDown(InteractKey))
                {   
                    key.gameObject.SetActive(false);
                    gc.OpenDoor();
                }
            }
        }
        
        else if(Physics.Raycast(transform.position, fwd, out hit, RayLenght, layer ))
        {   
            if(hit.collider.CompareTag("Note"))
            {   
                
                note = hit.collider.GetComponent<NoteController>();
                if(Input.GetKeyDown(InteractKey))
                {
                    gc.DisablePlayer();
                    gc.nc = note;
                    note.ShowPanel();
                }
            }
        }

        if(movement.rb.velocity.x > 0 || movement.rb.velocity.z > 0 || movement.rb.velocity.y > 0 ||
        movement.rb.velocity.x < -0.1f || movement.rb.velocity.z < -0.1f || movement.rb.velocity.y < -0.1)
        {
            Walking.enabled = true;
        }
        else{
            Walking.enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gc.DisablePlayer();
            gc.ui.ShowPauseMenu();
        }
    }

    
}
