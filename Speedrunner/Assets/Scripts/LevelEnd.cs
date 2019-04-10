using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelEnd : MonoBehaviour
{
    public GameManagement gameManager;
    public GameObject endScreen;

    public TMP_Text endTime;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            endScreen.SetActive(true);
            gameManager.levelFinished = true;
            endTime.text = gameManager.timePassed;
        }
    }
}
