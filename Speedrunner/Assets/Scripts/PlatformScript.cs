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
        transform.position = new Vector3(transform.position.x, transform.position.y + transform.parent.localScale.y / 2 - 0.05f, transform.position.z);
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

            if (gameManager.timePassedTicks < PlayerPrefs.GetInt("levelBest" + SceneManager.GetActiveScene().buildIndex)
                || PlayerPrefs.GetInt("levelBest" + SceneManager.GetActiveScene().buildIndex) == 0)
            {
                PlayerPrefs.SetInt("levelBest" + SceneManager.GetActiveScene().buildIndex, (int)gameManager.timePassedTicks);
              
                PlayerPrefs.SetString("levelBest" + SceneManager.GetActiveScene().buildIndex, gameManager.timePassed);
            }
        }
    }
}
