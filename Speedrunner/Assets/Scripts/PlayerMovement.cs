﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float crouchSpeed;
    public float acceleration;
    public float jumpHeight;
    public float maxSpeed;
    public float strafeAcceleration;
    public float fovMultiplier;

    public GameManagement gameManager;

    float sensitivity;
    float speed;
    float xRot;
    float t;

    Vector3 dir;
    Vector3 movement;

    Camera playerCamera;

    Rigidbody rigid;

    public TMP_Text speedText;

    void Start()
    {
        playerCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rigid = gameObject.GetComponent<Rigidbody>();

        sensitivity = PlayerPrefs.GetFloat("sensitivity");
    }

    void Update()
    {
        // MOUSE INPUT
        xRot += Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime * 100;

        xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);

        playerCamera.transform.localEulerAngles = new Vector3(-xRot, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z);
        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime * 100, 0));

        // SPEED CAP
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        // MOVEMENT
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            gameManager.stopwatch.Start();

            t = 0f;

            speed += Mathf.Abs(Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime * strafeAcceleration);

            if ((speed < movementSpeed || IsGrounded()) && !Input.GetButton("Crouch"))
            {
                speed = Mathf.Lerp(speed, movementSpeed, t += Time.deltaTime * acceleration);
            }

            // CROUCHING
            else if ((speed < movementSpeed || IsGrounded()) && Input.GetButton("Crouch"))
            {
                speed = Mathf.Lerp(speed, crouchSpeed, t += Time.deltaTime * acceleration);
            }

            if (Mathf.Abs(speed - movementSpeed) < 0.01f)
            {
                speed = movementSpeed;
            }

            dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        else
        {
            t = 0f;

            speed = Mathf.Lerp(speed, 0, t += Time.deltaTime * acceleration);

            if (speed < 0.01f)
            {
                speed = 0f;
            }
        }

        movement = dir.normalized * speed / 20;

        // JUMPING
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

        // SPEED TEXT
        speedText.text = ((int)(speed)).ToString();

        // CAMERA FOV
        playerCamera.fieldOfView = Mathf.Clamp(110 + speed / 200f * fovMultiplier, 110, 145);

        // CROUCHING
        if (Input.GetButton("Crouch"))
        {
            playerCamera.transform.localPosition = new Vector3(0, 0.9f, 0);
        }

        else
        {
            playerCamera.transform.localPosition = new Vector3(0, 1f, 0);
        }
    }

    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + transform.TransformDirection(movement) * Time.deltaTime);
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.001f) ||
            Physics.Raycast(transform.position + new Vector3(0.45f, 0, 0), Vector3.down, 1.001f) ||
            Physics.Raycast(transform.position + new Vector3(-0.45f, 0, 0), Vector3.down, 1.001f) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, 0.45f), Vector3.down, 1.001f) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, -0.45f), Vector3.down, 1.001f))
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}