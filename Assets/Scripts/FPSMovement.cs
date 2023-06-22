using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

/* 
 Look into different STATES, if the raycast detects a ledge wall, change "transform.forward (z axis)" to "transform.up (y axis)"
 make a thing that allows a button press to activate the "ledge wall mode", basically switching the player to moving across the Y axis

Make 2 Ground related raycasts - one that goes diagonally forward and detects the LedgeGround layer, allowing the player to move forward
and another that detects the LedgeGround layer directly below and turns gravity back on again.

Use the Ground Check in the FPSMovement script for this too.

USE RIGIDBODY STUFF!! - Probably make it "Is Kinematic" so it's controlled by script stuff.

The || is an "or"

Notes for raycasting!

- ONLY DO RAYCASTING STUFF AFTER TESTING WITH BUTTONS!!

Things to research:
- Rigidbodies and how to use them (apparently they dont work well with character controllers)
- Game states
*/

// This class will allow the Player's GameObject to move based on CharacterController 
public class FPSMovement : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode m_forward; // W
    public KeyCode m_back; // S
    public KeyCode m_left; // A
    public KeyCode m_right; // D
    public KeyCode m_sprint; // Left Shift
    public KeyCode m_jump; // Space

    //public KeyCode m_climb; // X
    //public KeyCode m_mount; // C

    [Header("Movement and Gravity")]
    public UnityEngine.CharacterController m_charControler; // Character Controller
    public float m_movementSpeed = 12f;
    public float m_runSpeed = 1.5f;
    private float m_finalSpeed = 0;
    public float m_gravity = -9.81f; // Default gravity number
    private Vector3 m_velocity; // Velocity is fall speed
    public float m_jumpHeight = 3f;

    public Transform m_groundCheckPoint;
    public float m_groundDistance = 0.4f;
    public LayerMask m_groundMask; // Ground layer
    //public LayerMask m_ledgeGroundMask; // LedgeGround layer
    private bool m_isGrounded; // Is the player touching the ground?

    [Header("Game States")]
    public bool isClimbing; // Starts the climbing thing where player goes up Y axis
    public bool isMounting; // Becomes true when a player is at the top of a ledge wall, allowing the player to walk forward but not fall yet

    // Head Raycast (from camera) - For ledge wall!
    [Header("Head Raycast")]
    public Transform h_rayPoint; // Camera position
    private Ray h_ray = new Ray(); // Defines ray
    private RaycastHit h_rayHit; // Get object hit
    public bool h_isHit = false; // Has the LEDGEWALL layer been hit?
    public LayerMask h_layerToHit; // Defining the layer that will be detected
    public float h_rayLength; // Length of ray

    // Foot Raycast (from below feet) - for unclimbing at the top of a ledge!
    [Header("Foot Raycast")]
    public Transform f_rayPoint; // The place the foot raycast happens from (transform)
    //private Ray f_ray = new Ray(); // Defines ray
    private RaycastHit f_rayHit; // Get object hit
    public bool f_isHit = false; // Has the GROUND layer been hit?
    public LayerMask f_layerToHit; // Defining the layer that will be detected
    public float f_rayLength; // Length of ray;
    

    // Awake is called before Start 
    void Awake()
    {
        m_finalSpeed = m_movementSpeed;
    }


    // Update is called once per frame 
    void Update()
    {
        m_isGrounded = HitGroundCheck(); // Checks touching the ground every frame
        

        if (m_isGrounded == false)
        {
            StartClimbRay(); // Check to see if there's a wall in front IF I am in the air.
            // You may want to see if you are in CLIMBING MODE first before you bother calling this.
        }

        

        MoveInputCheck(); // You want to know about states before move check


    }

    private void StartClimbRay() // The raycast that allows the player to start climbing Ledge Walls.
    {
        h_ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Creates the ray from mouse position. Only gets the direction of the ray
        Debug.DrawRay(h_rayPoint.transform.position, h_rayPoint.transform.forward);

        if (Physics.Raycast(h_ray, out h_rayHit, h_rayLength, h_layerToHit)) // Raycast function returns a boolean - returns an object hit to h_rayHit
        {
            h_isHit = true;
            isClimbing = true;           
            //Debug.Log("Climbing should start");
            Debug.DrawRay(h_rayPoint.transform.position, h_rayPoint.transform.forward, Color.red);


            EndClimbRay();
        }
    }

    private void EndClimbRay() // Once the player is at the top of a Ledge Wall, this raycast allows them to walk forward.
    {
        if (isClimbing)
        {
            h_rayLength = 5f;
            h_layerToHit = 3;

            Ray f_ray = new Ray(f_rayPoint.transform.position, transform.forward * f_rayLength);
            Debug.DrawRay(f_rayPoint.transform.position, f_rayPoint.transform.forward * f_rayLength);

            if (Physics.Raycast(f_ray, out f_rayHit, f_rayLength, f_layerToHit))
            {
                f_isHit = true;
                Debug.Log("Foot ray hit ground");
                Debug.DrawRay(f_rayPoint.transform.position, f_rayPoint.transform.forward * f_rayLength, Color.red);

                isMounting = true;
            }
        }
    }

    public void MoveInputCheck() // EVERYTHING IN HERE IS ABOUT TAKING IN INPUT. THAT'S IT - IT JUST GETS WHAT MOVEMENT TO ADD, BUT DOESN'T APPLY IT UNTIL 'MovePlayer'
    {
        float x = Input.GetAxis("Horizontal"); // Gets the x input value  
        float z = Input.GetAxis("Vertical"); // Gets the z input value  

        Vector3 move = Vector3.zero;

        /*if (Input.GetKeyDown(m_climb))
        {
            isClimbing = true;
        }

        if (Input.GetKeyDown(m_mount))
        {
            isMounting = true;
        }*/

        if (isClimbing == true)
        {
            if (Input.GetKey(m_forward)) 
            {
                // STUFF HERE ABOVE CLIMBING MOVEMENT
                move = transform.up * z; // Changes W key to move up on Y axis instead of forward
                m_gravity = 0f; // Freezes gravity
                m_velocity.y = 0f;
            }
            else if (Input.GetKey(m_back))
            {
                isClimbing = false; // Undoes climb mode
                h_isHit = false; // Raycast unhits
                m_gravity = -9.81f; // Gravity reenables
            }
        }
        else if (Input.GetKey(m_forward) || Input.GetKey(m_back) || Input.GetKey(m_left) || Input.GetKey(m_right))
        {
            move = transform.right * x + transform.forward * z; // calculate the move vector (direction)
        }        

        if (isMounting == true)
        {
            if (Input.GetKey(m_forward))
            {
                // Mounting movement - Should either reenable gravity or allow the player to walk forward
                move = transform.forward * z;
                m_gravity = -9.81f;
            }
            else if (m_isGrounded)
            {
                isMounting = false;
                isClimbing = false;
                f_isHit = false;                
            }
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

        if (isMounting)
        {
            move = transform.forward;
            m_charControler.Move(m_velocity * Time.deltaTime);
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
