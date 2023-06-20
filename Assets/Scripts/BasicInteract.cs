using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Runtime.CompilerServices;

/*
[Serializable]
public class IntEvent : UnityEvent<int> { }

public class BasicInteract : MonoBehaviour
{
    // RAYCAST VARS
    [Header("Raycast Settings")]
    private Ray g_ray = new Ray();
    public RaycastHit hitObject;
    public LayerMask layerToHit;
    //public GraphicRaycaster raycaster;
    public float rayLength = 5f; // Adjust this if you want to adjust the 'click to grab' distance
    public Image CrosshairDot;

    // VARS FOR RAYCAST RESULTS - ALL ABOUT SHOWING BOOL RESULTS, NOT SETTING THEM!
    [Header("Results")]
    public bool rayHit;
    public bool canInteract; // a blanket check of if you can do SOMETHING with an object under the crosshair.
    public GameObject interactiveObject; // what it is your crosshair is on
    public bool targetIsInteractive; // is the thing an interactive device?

    // PROMPT SYSTEM VARS - All about options for showing prompts, and also for what text is shown when they fire.
    [Header("Prompt Options")]
    public bool interactablePromptsText; // These are all a checkbox to see IF the player wants a popup or not.
    public Text intPromptTxt;
    public string intMessage;

    void Start()
    {
        rayHit = false;
        canInteract = false;
        CrosshairDot = GameObject.Find("CrosshairDot").GetComponent<Image>();
    }

    void Update()
    {
        g_ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast From Mouse Position 
        if (Physics.Raycast(g_ray, out hitObject, rayLength, layerToHit)) // If raycast hits collider... 
        {
            if (hitObject.collider.tag == "Interact") // If that collider has 'Interact' tag - Implies a switch / device, not an object to pick up
            {
                // THIS STUFF IS ABOUT FIRING PROMPTS FOR SOMETHING YOU CAN INTERACT WITH IN THE WORLD
                rayHit = true;
                interactiveObject = hitObject.collider.gameObject;
                targetIsInteractive = true;
                if (interactablePromptsText)
                {
                    intPromptTxt.enabled = true;
                    intPromptTxt.text = intMessage;
                }

            }
        }
        else // IF RAYCAST DOESN'T HIT ONE OF THOSE THINGS, RESET RAYCAST AND DEACTIVATE ALL PROMPTS
        {
            //ResetPrompts();

            rayHit = false;
            interactiveObject = null;
            canInteract = false;
            targetIsInteractive = false;
            intPromptTxt.enabled = false;
        }

        if (rayHit == true) //Turns the Crosshair green
        {
            //Debug.Log("RaycastHit");
            CrosshairDot.GetComponent<Renderer>();
            CrosshairDot.color = Color.green;
            canInteract = true;
        }

        if (rayHit == false) // resets colour of crosshair
        {
            CrosshairDot.GetComponent<Renderer>();
            CrosshairDot.color = Color.white;
            canInteract = false;
        }

        // THIS IS WHERE ACTUAL INTERACTIONS HAPPEN, AND RESULT IN CALLING FUNCTIONS APPROPRIATE FOR THAT.

        if (canInteract == true)
        {
            if (targetIsInteractive)
            {
                interactiveObject.GetComponent<Interactable>().TriggerEvent(); // ACTIVE THE 'TriggerEvent' FUNCTION ON THE OBJECT.
            }
        }
    }
}
*/