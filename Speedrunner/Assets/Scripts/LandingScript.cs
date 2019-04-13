using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingScript : MonoBehaviour
{
    public AudioSource landingSound;
    public PlayerMovement playerMovement;

    void OnTriggerEnter(Collider coll)
    {
        playerMovement.isGrounded = true;

        if (Time.timeSinceLevelLoad > 0.3f && coll.gameObject.CompareTag("Platform"))
        {
            landingSound.Play();
        }
    }

    void OnTriggerExit(Collider coll)
    {
        playerMovement.isGrounded = false;
    }
}
