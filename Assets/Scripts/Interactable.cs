using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject doorOne; // Door
    public DoorOpener doorOpener; // Script that actually opens door

    public void ButtonPress()
    {
        doorOpener.DoorOpening(); // If player has interacted with button, use other script to open door
    }
}