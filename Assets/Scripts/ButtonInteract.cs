using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
For buttons, instead of using Event Systems, take the "Interactable" script from zombie game, and make remove the Event thing.
Change it to make it so that, if it's clicked, it just checks if a function of being clicked is true and if it is, door thing moves.
*/


public class ButtonInteract : MonoBehaviour
{
    private Ray b_ray = new Ray(); // Define a ray for this check
    private RaycastHit b_hitObject; // Use the RaycastHit type to get an object hit
    private bool b_isHit = false;
    public LayerMask b_layerToHit; // Defining a layer that will be detected with our raycast
    public float b_rayLength = 10f; // Length of the ray
    public KeyCode b_boundKey; // Left Mouse Button
    public UnityEvent b_onObjectClicked; // store a callback event to some other function

    void Update()
    {
        if (Input.GetKeyDown(b_boundKey))
            CastRay();
        else if (Input.GetKeyUp(b_boundKey))
            b_isHit = false;
    }
    private void CastRay()
    {
        b_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Creates a ray from the camera at the X & Y point of the mouse position
        // Only really gets the direction of the ray - 'point to' <thing>

        // Raycast function returns a boolean - returns an object hit to b_hitObject 
        if (Physics.Raycast(b_ray, out b_hitObject, b_rayLength, b_layerToHit))
        {
            if (b_isHit == false)
            {
                b_onObjectClicked?.Invoke(); // if not not - run the function that the event is pointing to
                b_isHit = true;
            }
        }
    }

    private void ButtonPress()
    {
        if (b_isHit == true)
        {

        }
    }
}
