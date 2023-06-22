using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject doorOne;
    public bool openDoor; // A hack that is basically "tick this box and see if door open". Also used to start door opening function
    public DoorOpener doorOpener;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress()
    {
        //doorOne.GetComponent<DoorOpener>().DoorOpening(); // You TOTALLY need this!
        doorOpener.DoorOpening(); //THIS WOULD BE SIMPLER :)
    }
}
