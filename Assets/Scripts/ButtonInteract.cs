using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Runtime.CompilerServices;

public class ButtonInteract : MonoBehaviour
{
    // Raycast Setup Variables    
    private Ray b_ray = new Ray(); // Defines the button raycast
    private RaycastHit b_hitObject; // Checks if the ray has hit an object    
    public float b_rayLength = 5f; // Length of the ray
    public KeyCode b_boundKey; // Left Mouse Button (Mouse 0 in Unity)
    public Image Crosshair;
    public GameObject buttonObject; // The actual button game object in unity
    public bool b_didHit; // A bool to show if the raycast hit the button - mostly for debugging

    void Start()
    {
        Crosshair = GameObject.Find("CrosshairDot").GetComponent<Image>(); // Sets up the Crosshair UI for the player
    }

    void Update()
    {
        b_ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Sets the ray start point and direction
        if (Physics.Raycast(b_ray, out b_hitObject, b_rayLength)) // Raycast from where the camera is looking
        {
            if (b_hitObject.collider.tag == "Button") // If raycast hits collider...
            {                
                //Debug.Log("Button ray hit");

                // Turns crosshair green
                Crosshair.GetComponent<Renderer>();
                Crosshair.color = Color.green;

                buttonObject = b_hitObject.collider.gameObject; // Makes the button's collider interactive

                // If the player clicks Left Mouse Button, activate the button
                if (Input.GetKeyDown(b_boundKey))
                {
                    buttonObject.GetComponent<Interactable>().ButtonPress(); // Calls the scripts that activate the button and open the door
                }
            }            
        }
        else
        {
            // Turns crosshair back to white when the ray isn't hitting the button
            Crosshair.GetComponent<Renderer>();
            Crosshair.color = Color.white;

            buttonObject = null; // Makes it so the player cant press the button while looking away from it
        }
    }
}