using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlatformScript : MonoBehaviour
{
    public GameManagement gameManager;
    public GameObject endScreen;

    public TMP_Text endTime;

    void Start()
    {
        transform.localScale = new Vector3(1, 0.1f / transform.lossyScale.y, 1);
        transform.localPosition = new Vector3(0, 0.5f, 0);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player" && gameObject.tag == "Finish")
        {
            Time.timeScale = 0;

            endScreen.SetActive(true);
            gameManager.levelFinished = true;
            endTime.text = gameManager.timePassed;

            PlayerPrefs.SetInt("level" + SceneManager.GetActiveScene().buildIndex + 1, 1);
        }
    }
}
