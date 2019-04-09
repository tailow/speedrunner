using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float acceleration;
    public float sensitivity;
    public float jumpHeight;
    public float maxSpeed;
    public float strafeAcceleration;
    public float fovMultiplier;

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
    }

    void Update()
    {
        // MOUSE INPUT
        xRot += Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime * 100;

        xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);

        playerCamera.transform.localEulerAngles = new Vector3(-xRot, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z);
        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime * 100, 0));

        // MOVEMENT
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            t = 0f;

            speed += Mathf.Abs(Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime * strafeAcceleration);

            if (speed < movementSpeed || IsGrounded())
            {
                speed = Mathf.Lerp(speed, movementSpeed, t += Time.deltaTime * acceleration);
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

        movement = dir.normalized * speed;

        // JUMPING
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

        // SPEED TEXT
        speedText.text = ((int)(speed * 20)).ToString();

        // CAMERA FOV
        playerCamera.fieldOfView = Mathf.Clamp(120 + speed * 0.1f * fovMultiplier, 120, 160);
    }

    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + transform.TransformDirection(movement) * Time.deltaTime);
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.001f) ||
            Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector3.down, 1.001f) ||
            Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), Vector3.down, 1.001f) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, 0.5f), Vector3.down, 1.001f) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, -0.5f), Vector3.down, 1.001f))
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}