using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMotor : MonoBehaviour
{
    CharacterController characterController;
    Transform transform;
    public float mouseSensitivity = 5.0f; // Sensitivity of mouse movement
    private float rotationX = 0.0f;
    float playerSpeed = 10f;
    public float gravity = 1.0f; // Gravity force
    public float jumpForce = 18.0f; // Force of the jump
    Vector3 verticalMoveDirection = Vector3.zero;

    void Start()
    {
        transform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //hareket yönü
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = Vector3.forward;          
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection = Vector3.back;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection = Vector3.left;
        }
        //Gerçek hareket kýsmý
        if (moveDirection != Vector3.zero)
        {
            characterController.Move(transform.TransformDirection(moveDirection) * playerSpeed * Time.deltaTime);
        }

        //mouse bakýþ
        float mouseSagSol = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseIleriGeri = Input.GetAxis("Mouse Y") * mouseSensitivity;
       
        //sað sol bakýþ
        transform.Rotate(0, mouseSagSol, 0);

       // Kamera dikey dönüþ
        rotationX -= mouseIleriGeri;
        rotationX = Mathf.Clamp(rotationX, -80, 80); // Limit vertical rotation to avoid flipping

      

        // kamera dikey dönüþ uygula.
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        

        //zýplama ve yerçekimi       
        if (characterController.isGrounded)
        {
            // Apply gravity only when the character is not jumping
            verticalMoveDirection.y = 0;

            // Check for jump input
            if (Input.GetKey(KeyCode.Space))
            {
                // Apply jump force
                verticalMoveDirection.y += jumpForce * Time.deltaTime;
                //  Debug.Log(moveDirection);
            }
            //  Debug.Log(moveDirection.ToString());
        }
       
        // Apply gravity
        verticalMoveDirection.y -= gravity * Time.deltaTime;

        // Move the character
        characterController.Move(verticalMoveDirection * Time.deltaTime);



    }

}
