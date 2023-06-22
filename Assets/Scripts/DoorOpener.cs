using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public ButtonInteract buttonInt;
    public Interactable intScript;

    public Renderer doorRend;
    public Collider doorCollider;

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

    // Update is called once per frame
    void Update()
    {

    }

    public void DoorOpening()
    {
        doorRend.enabled = false;
        doorCollider.enabled = false;
    }
}
