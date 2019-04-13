using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingScript : MonoBehaviour
{
    public AudioSource landingSound;
    public PlayerMovement playerMovement;

    void OnTriggerEnter(Collider coll)
    {
        if (Time.timeSinceLevelLoad > 0.3f && coll.gameObject.CompareTag("Platform"))
        {
            playerMovement.isGrounded = true;

            landingSound.Play();
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Platform"))
        {
            playerMovement.isGrounded = false;
        }
    }
}
