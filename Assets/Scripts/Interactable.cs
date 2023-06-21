using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public ButtonInteract buttonInt; // References the ButtonInteract script as a whole
    public KeyCode interactKey; // Meant to reference "b_boundKey" from the ButtonInteract script

    public bool openDoor; // A hack that is basically "tick this box and see if door open". Also used to start door opening function



    // Start is called before the first frame update
    void Start()
    {
        buttonInt = GetComponent<ButtonInteract>();
        interactKey = GetComponent<ButtonInteract>().b_boundKey;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress()
    {
        if (buttonInt.GetComponent<ButtonInteract>().b_canInteract == true)
        {
            if (Input.GetKeyDown(interactKey))
            {
                
            }
        }
    }
}
