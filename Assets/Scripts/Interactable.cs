using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public ButtonInteract buttonInt;
    public KeyCode buttonPressKey;



    // Start is called before the first frame update
    void Start()
    {
        buttonInt = GetComponent<ButtonInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress()
    {
        if (buttonInt.GetComponent<ButtonInteract>().b_canInteract == true)
        {
            if (Input.GetKeyDown(buttonPressKey))
            {

            }
        }
    }
}
