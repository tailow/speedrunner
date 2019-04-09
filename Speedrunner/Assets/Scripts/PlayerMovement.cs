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

    float speed;
    float xRot;
    float t;

    Vector3 dir;
    Vector3 movement;

    GameObject playerCamera;

    Rigidbody rigid;

    public TMP_Text speedText;

    void Start()
    {
        playerCamera = Camera.main.gameObject;

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

            speed = Mathf.Lerp(speed, movementSpeed, t += Time.deltaTime * acceleration);

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
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

        // SPEED TEXT
        speedText.text = ((int)(speed * 100)).ToString();
    }

    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + transform.TransformDirection(movement) * Time.deltaTime);
    }
}
