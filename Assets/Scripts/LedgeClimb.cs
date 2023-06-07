using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LedgeClimb : MonoBehaviour
{
    private Ray h_ray = new Ray(); // Define a ray for this check
    private RaycastHit h_rayHit; // Use the RaycastHit type to get an object hit
    private bool h_isHit = false;

    public LayerMask h_layerToHit; // Defining a layer that will be detected with our raycast
    public float h_rayLength = 10f; // Length of the ray

    public KeyCode h_boundKey; // store the key that executes raycast
    public UnityEvent h_onClick; // store a callback event to some other function


    void Update()
    {
        if (Input.GetKeyDown(h_boundKey))
            CastRay();
        else if (Input.GetKeyUp(h_boundKey))
            h_isHit = false;
    }
    private void CastRay()
    {
        h_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Creates a ray from the camera at the X & Y point of the mouse position
        // Only really gets the direction of the ray - 'point to' <thing>
        // Raycast function returns a boolean - returns an object hit to g_hitObject 
        if (Physics.Raycast(h_ray, out h_rayHit, h_rayLength, h_layerToHit))
        {
            if (h_isHit == false)
            {
                h_onClick?.Invoke(); // if not not - run the function that the event is pointing to
                h_isHit = true;
            }
        }
    }
}
