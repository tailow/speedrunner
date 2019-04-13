using System.Collections;
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
    public float sensitivity;
    public float jumpDelay;

    public bool isGrounded;

    public GameManagement gameManager;

    public AudioSource jumpSound;

    float lastJump;
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
        isGrounded = true;

        playerCamera = Camera.main;

        rigid = gameObject.GetComponent<Rigidbody>();

        if (PlayerPrefs.GetInt("sensitivity") == 0)
        {
            sensitivity = 10;
        }

        else
        {
            sensitivity = PlayerPrefs.GetInt("sensitivity");
        }
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

            if ((speed < movementSpeed || isGrounded) && !Input.GetButton("Crouch"))
            {
                speed = Mathf.Lerp(speed, movementSpeed, t += Time.deltaTime * acceleration);
            }

            // CROUCHING
            else if ((speed < movementSpeed || isGrounded) && Input.GetButton("Crouch"))
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
        if (Input.GetButtonDown("Jump") && (Time.time - lastJump > jumpDelay))
        {
            if (isGrounded)
            {
                isGrounded = false;

                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);

                jumpSound.Play();
            }

            lastJump = Time.time;
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
}