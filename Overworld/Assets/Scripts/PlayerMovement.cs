using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //THESE ARE THE VALUES THAT MATTER FOR PROGRESSION! DONT TOUCH, LEAVE TO MICHAEL
    public string name = "LeeroyJankins"; //player name TODO: let player decide the name for themselves
    public int score = 0; //Shows how many points you have TODO: put this in some progress bar or something
    public int accessLevel = 0; //What decides which levels you are allowed to play. you can assign the needed level in the trigger boxes.

    //THESE ARE VALUES FOR QUALITY OF LIFE
    public float xx = 0; //player x position
    public float yy = 0; //player y position
    public float zz = 0; //player x position

    //THESE ARE THE VALUES FOR MOVEMENT, DONT TOUCH UNLESS YOU KNOW WHAT YOU ARE DOING
    public Camera playerCamera;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 0.5f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 1f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get input as raw world-space direction
        float inputX = Input.GetAxisRaw("Horizontal"); // A/D
        float inputZ = Input.GetAxisRaw("Vertical");   // W/S

        Vector3 input = new Vector3(inputX, 0f, inputZ).normalized;

        // Determine speed (run vs walk)
        bool isRunning = false; //Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        moveDirection = input * currentSpeed;

        // Apply movement
        characterController.SimpleMove(moveDirection); // Automatically applies gravity = 9.81 internally

        // Rotate to face movement direction
        if (input.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Update debug position tracking
        Vector3 position = transform.position;
        xx = position.x;
        yy = position.y;
        zz = position.z;
    }
}
