using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/*
For buttons, instead of using Event Systems, take the "Interactable" script from zombie game, and make remove the Event thing.
Change it to make it so that, if it's clicked, it just checks if a function of being clicked is true and if it is, door thing moves.
*/


public class ButtonInteract : MonoBehaviour
{
    // Raycast setup
    [Header("Raycast Settings")]
    private Ray b_ray = new Ray(); // Define a ray for this check
    private RaycastHit b_hitObject; // Use the RaycastHit type to get an object hit
    private bool b_isHit = false;
    public LayerMask b_layerToHit; // Defining a layer that will be detected with our raycast
    public float b_rayLength = 5f; // Length of the ray
    public KeyCode b_boundKey; // Left Mouse Button
    public UnityEvent b_onObjectClicked; // store a callback event to some other function
    public Image CrosshairDot;

    // Raycast Results - For SHOWING bool results, not setting them
    [Header("Results")]
    public bool b_didHit;
    public bool b_canInteract;
    public GameObject interactiveObject;
    public bool targetIsInteractive;

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
                b_didHit = true;
                interactiveObject = b_hitObject.collider.gameObject;
                targetIsInteractive = true;
            }
            else
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
        }


    }

    private void ButtonPress()
    {
        if (b_isHit == true)
        {

        }
    }
}
