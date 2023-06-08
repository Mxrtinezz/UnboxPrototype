using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 Look into different STATES, if the raycast detects a ledge wall, change "transform.forward (z axis)" to "transform.up (y axis)"
 make a thing that allows a button press to activate the "ledge wall mode", basically switching the player to moving across the Y axis

Make 2 Ground related raycasts - one that goes diagonally forward and detects the Ground layer, allowing the player to move forward
and another that detects the Ground layer directly below and turns gravity back on again.

Use the Ground Check in this script for this too.

USE RIGIDBODY STUFF!!
*/


// This class will allow the Player's GameObject to move based on CharacterController 
public class FPSMovement : MonoBehaviour

{
    public KeyCode m_forward; // W
    public KeyCode m_back; // S
    public KeyCode m_left; // A
    public KeyCode m_right; // D
    public KeyCode m_up; // F
    
    public UnityEngine.CharacterController m_charControler;
    public float m_movementSpeed = 12f;
    
    public float m_gravity = -9.81f;
    private Vector3 m_velocity;
    
    private float m_finalSpeed = 0;

    public float m_runSpeed = 1.5f;
    public KeyCode m_sprint;

    public float m_jumpHeight = 3f;
    public Transform m_groundCheckPoint;
    public float m_groundDistance = 0.4f;
    public LayerMask m_groundMask;

    private bool m_isGrounded;
    public KeyCode m_jump;


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
    }

    void MoveInputCheck()
    {
        float x = Input.GetAxis("Horizontal"); // Gets the x input value  
        float z = Input.GetAxis("Vertical"); // Gets the z input value  

        Vector3 move = Vector3.zero;

        if (Input.GetKey(m_forward) || Input.GetKey(m_back) || Input.GetKey(m_left) || Input.GetKey(m_right))
        {
            move = transform.right * x + transform.forward * z; // calculate the move vector (direction)
        }
        
        else if (Input.GetKey(m_up))
        {

        }

        MovePlayer(move); // Run the MovePlayer function with the vector3 value move 
        RunCheck(); // Checks the input for run 
        JumpCheck(); // Checks if we can jump 
    }

    void MovePlayer(Vector3 move)
    {
        m_charControler.Move(move * m_finalSpeed * Time.deltaTime); // Moves the Gameobject using the Character Controller 
        m_velocity.y += m_gravity * Time.deltaTime; // Gravity affects the jump velocity 
        m_charControler.Move(m_velocity * Time.deltaTime); //Actually move the player up 
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
