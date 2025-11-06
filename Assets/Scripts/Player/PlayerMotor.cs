using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Boolean isGrounded;

    private bool canMove = true;
    public float gravity = -9.81f;
    public float playerSpeed = 5f;
    public float jumpHeight = 0.14f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    //revieve input from InputManager and apply them to our character controller
    public void ProcessMove(Vector2 input)
    {
        if (!canMove) return;

        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * playerSpeed * Time.deltaTime);
        //gravity
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;   
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -15f * gravity);
        }
    }

    public void Teleport(Vector3 position, Quaternion rotation)
    {
        transform.localPosition = position;
        transform.localRotation = rotation;
        Physics.SyncTransforms();
        playerVelocity = Vector3.zero;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;

        if (!canMove)
        {
            // stop any remaining movement velocity
            playerVelocity = Vector3.zero;
        }
    }
}
