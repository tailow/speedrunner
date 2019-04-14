using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlatformScript : MonoBehaviour
{
    public GameManagement gameManager;
    public GameObject endScreen;
    public PlayerMovement movementScript;
    public GameObject newRecordText;

    public TMP_Text endTime;

    void Start()
    {
        if (gameObject.tag == "Platform" || gameObject.tag == "Finish")
        {
            transform.localScale = new Vector3(1, 0.2f / transform.lossyScale.y, 1);
            transform.position = new Vector3(transform.position.x, transform.position.y + transform.parent.localScale.y / 2 - 0.1f, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player" && gameObject.tag == "Finish")
        {
            Time.timeScale = 0;

            endScreen.SetActive(true);
            gameManager.levelFinished = true;
            endTime.text = gameManager.timePassed;

            PlayerPrefs.SetInt("level" + SceneManager.GetActiveScene().buildIndex, 1);

            if (gameManager.timePassedTicks < PlayerPrefs.GetInt("levelBestTicks" + SceneManager.GetActiveScene().buildIndex)
                || PlayerPrefs.GetInt("levelBestTicks" + SceneManager.GetActiveScene().buildIndex) == 0)
            {
                PlayerPrefs.SetInt("levelBestTicks" + SceneManager.GetActiveScene().buildIndex, (int)gameManager.timePassedTicks);
              
                PlayerPrefs.SetString("levelBest" + SceneManager.GetActiveScene().buildIndex, gameManager.timePassed);

                newRecordText.SetActive(true);
            }

            else
            {
                newRecordText.SetActive(false);
            }
        }

        if (coll.tag == "Player" && gameObject.tag == "Booster")
        {
            movementScript.speed += 500;
        }
    }
}
