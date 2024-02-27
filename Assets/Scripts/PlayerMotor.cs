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
        //hareket y�n�
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
        //Ger�ek hareket k�sm�
        if (moveDirection != Vector3.zero)
        {
            characterController.Move(transform.TransformDirection(moveDirection) * playerSpeed * Time.deltaTime);
        }

        //mouse bak��
        float mouseSagSol = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseIleriGeri = Input.GetAxis("Mouse Y") * mouseSensitivity;
       
        //sa� sol bak��
        transform.Rotate(0, mouseSagSol, 0);

       // Kamera dikey d�n��
        rotationX -= mouseIleriGeri;
        rotationX = Mathf.Clamp(rotationX, -80, 80); // Limit vertical rotation to avoid flipping

      

        // kamera dikey d�n�� uygula.
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        

        //z�plama ve yer�ekimi       
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
