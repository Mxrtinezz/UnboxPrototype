using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Runtime.CompilerServices;

/*
For buttons, instead of using Event Systems, take the "Interactable" script from zombie game, and make remove the Event thing.
Change it to make it so that, if it's clicked, it just checks if a function of being clicked is true and if it is, door thing moves.
*/


public class ButtonInteract : MonoBehaviour
{
    // Raycast Setup Variables
    [Header("Raycast Settings")]
    private Ray b_ray = new Ray(); // Define a ray for this check
    private RaycastHit b_hitObject; // Use the RaycastHit type to get an object hit
    private bool b_isHit = false;
    public LayerMask b_layerToHit; // Defining a layer that will be detected with our raycast
    public float b_rayLength = 5f; // Length of the ray
    public KeyCode b_boundKey; // Left Mouse Button
    public UnityEvent b_onObjectClicked; // store a callback event to some other function
    public Image CrosshairDot;

    // Raycast Results Variables - For SHOWING bool results, not setting them
    [Header("Results")]
    public bool b_didHit; // Shows if the raycast did actually hit the collider
    public bool b_canInteract;
    public GameObject interactiveObject;
    public bool targetIsInteractive;

    // Prompt System Variables
    [Header("Prompt Options")]
    public bool interactablePromptsText; // These are all a checkbox to see IF the player wants a popup or not.
    public Text intPromptText;
    public string intMessage;

    void Start()
    {
        b_didHit = false;
        b_canInteract = false;
        CrosshairDot = GameObject.Find("CrosshairDot").GetComponent<Image>(); // Finds the CrosshairDot UI object in Unity
    }

    void Update()
    {
        b_ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast from mouse position
        if (Physics.Raycast(b_ray, out b_hitObject, b_rayLength, b_layerToHit)) // If raycast hits collider...
        {
            if (b_hitObject.collider.tag == "Button") // If that collider has the "Button" tag
            {
                // This stuff is about firing prompts for something you can interact with in the world (in this case, button)
                b_didHit = true;
                interactiveObject = b_hitObject.collider.gameObject;
                targetIsInteractive = true;
                if (interactablePromptsText)
                {
                    intPromptText.enabled = true;
                    intPromptText.text = intMessage;
                }
            }
            else // If raycast doesn't hit one of those things, reset raycast and deactivate all prompts
            {
                b_didHit = false;
                interactiveObject = null;
                b_canInteract = false;
            }
        }

        if (b_didHit == true) // Turns crosshair green
        {
            CrosshairDot.GetComponent<Renderer>();
            CrosshairDot.color = Color.green;
            b_canInteract = true;
            targetIsInteractive = false;
            intPromptText.enabled = false;
        }

        if (b_didHit == false) // Resets colour of crosshair
        {
            CrosshairDot.GetComponent<Renderer>();
            CrosshairDot.color = Color.white;
            b_canInteract = false;
        }

        // INTERACTIONS - This is where the ACTUAL interactions happen, and result in calling the relative functions

        if (b_canInteract == true)
        {
            if (targetIsInteractive)
            {
                interactiveObject.GetComponent<Interactable>().ButtonPress(); 
            }
        }


    }
}
