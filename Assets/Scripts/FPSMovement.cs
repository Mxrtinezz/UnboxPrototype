using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

/* 
 Look into different STATES, if the raycast detects a ledge wall, change "transform.forward (z axis)" to "transform.up (y axis)"
 make a thing that allows a button press to activate the "ledge wall mode", basically switching the player to moving across the Y axis

Make 2 Ground related raycasts - one that goes diagonally forward and detects the Ground layer, allowing the player to move forward
and another that detects the Ground layer directly below and turns gravity back on again.

Use the Ground Check in the FPSMovement script for this too.

USE RIGIDBODY STUFF!! - Probably make it "Is Kinematic" so it's controlled by script stuff.

The || is an "or"

Notes for raycasting!

- ONLY DO RAYCASTING STUFF AFTER TESTING WITH BUTTONS!!

Things to research:
- How to freeze gravity
- How to switch axis movement
- Rigidbodies and how to use them (apparently they dont work well with character controllers)
- Game states
*/


// This class will allow the Player's GameObject to move based on CharacterController 
public class FPSMovement : MonoBehaviour

{

    public KeyCode m_forward; // W
    public KeyCode m_back; // S
    public KeyCode m_left; // A
    public KeyCode m_right; // D


    public UnityEngine.CharacterController m_charControler;
    public float m_movementSpeed = 12f;
    
    public float m_gravity = -9.81f; // Gravity number
    private Vector3 m_velocity; // Velocity is fall speed
    
    private float m_finalSpeed = 0;

    public float m_runSpeed = 1.5f;
    public KeyCode m_sprint;

    public float m_jumpHeight = 3f;
    public Transform m_groundCheckPoint;
    public float m_groundDistance = 0.4f;
    public LayerMask m_groundMask; // Ground layer

    private bool m_isGrounded;
    public KeyCode m_jump; // Space

    public bool isClimbing; // Starts the climbing thing where player goes up Y axis



    // Head Raycast (from camera) - For ledge wall!
    private Ray h_ray = new Ray(); // Define a ray for this check
    private RaycastHit h_rayHit; // Use the RaycastHit type to get an object hit
    public bool h_isHit = false; // Has the layer been hit?

    public LayerMask h_layerToHit; // Defining a layer that will be detected with our raycast
    public float h_rayLength; // Length of the ray



    // Awake is called before Start 
    void Awake()
    {
        m_finalSpeed = m_movementSpeed;
    }


    // Update is called once per frame 
    void Update()
    {
        m_isGrounded = HitGroundCheck(); // Checks touching the ground every frame
        MoveInputCheck();

        if (m_isGrounded == false)
        {
            CastRay();
        }
    }

    private void CastRay()
    {
        h_ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Creates the ray from mouse position. Only gets the direction of the ray (whatever that's supposed to mean)
        
        if (Physics.Raycast(h_ray, out h_rayHit, h_rayLength, h_layerToHit)) // Raycast function returns a boolean - returns an object hit to h_rayHit
        {
            h_isHit = true;
            Debug.Log("The ray hit i think maybe");

            if (h_isHit == true)
            {
                isClimbing = true;
            }
            else
            {
                h_isHit = false;
            }
        }

    }

    public void MoveInputCheck() // EVERYTHING IN HERE IS ABOUT TAKING IN INPUT. THAT'S IT - IT JUST GETS WHAT MOVEMENT TO ADD, BUT DOESN'T APPLY IT UNTIL 'MovePlayer'
    {
        float x = Input.GetAxis("Horizontal"); // Gets the x input value  
        float z = Input.GetAxis("Vertical"); // Gets the z input value  

        Vector3 move = Vector3.zero;


        if (isClimbing == true)
        {
            if (Input.GetKey(m_forward)) 
            {
                // STUFF HERE ABOVE CLIMBING MOVEMENT
                move = transform.up * z;
                m_gravity = 0f;
                m_velocity.y = 0f;
            }
            else if (Input.GetKey(m_back))
            {
                isClimbing = false;
                m_gravity = -9.81f;
            }
        }
        else if (Input.GetKey(m_forward) || Input.GetKey(m_back) || Input.GetKey(m_left) || Input.GetKey(m_right))
        {
            move = transform.right * x + transform.forward * z; // calculate the move vector (direction)
        }        

        MovePlayer(move); // Run the MovePlayer function with the vector3 value move 
        RunCheck(); // Checks the input for run 
        JumpCheck(); // Checks if we can jump 
    }



    void MovePlayer(Vector3 move)
    {
        m_charControler.Move(move * m_finalSpeed * Time.deltaTime); // Moves the Gameobject using the Character Controller 
        
        // Next two lines are NORMAL movement when not climbing... You need to make two paths here - One with gravity, one without.
        m_velocity.y += m_gravity * Time.deltaTime; // Gravity affects the jump velocity 
        m_charControler.Move(m_velocity * Time.deltaTime); //Actually move the player up 

        if (isClimbing)
        {
            move = transform.up;
            m_charControler.Move(m_velocity * Time.deltaTime); // Actually move the player up
        }
    }

    void RunCheck()
    {
        if (Input.GetKeyDown(m_sprint)) // if key is down, sprint
        {
            m_finalSpeed = m_movementSpeed * m_runSpeed;
        }

        else if (Input.GetKeyUp(m_sprint)) // if key is up, don't sprint
        {
            m_finalSpeed = m_movementSpeed;
        }
    }

    void JumpCheck()
    {
        if (Input.GetKeyDown(m_jump)) // If the player presses space (make sure this is right in Unity)
        {
            if (m_isGrounded == true) // If the player is touching the ground
            {
                m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity); // Defines the jump and how high the player can jump

                
            }
        }
    }

    bool HitGroundCheck()
    {
        bool isGrounded = Physics.CheckSphere(m_groundCheckPoint.position, m_groundDistance, m_groundMask);

        // Gravity
        if (isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = -4f;
        }

        return isGrounded;
    }

}
