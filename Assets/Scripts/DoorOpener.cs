using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public ButtonInteract buttonInt; // The ButtonInteract script as a whole
    public Interactable intScript; // The Interactable script as a whole
    public Renderer doorRend; // The door's mesh renderer
    public Collider doorCollider; // The door's collider

    // Start is called before the first frame update
    void Start()
    {
        buttonInt = GetComponent<ButtonInteract>();
        intScript = GetComponent<Interactable>(); 
        doorRend = GetComponent<Renderer>();
        doorCollider = GetComponent<Collider>();

        doorCollider.enabled = true;
        doorRend.enabled = true;
    }

    public void DoorOpening()
    {
        // When button is interacted with, the mesh renderer and collider disable
        doorRend.enabled = false;
        doorCollider.enabled = false;
    }
}