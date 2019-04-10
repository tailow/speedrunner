using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagement : MonoBehaviour
{
    public Stopwatch stopwatch;

    public TMP_Text stopwatchText;

    public string timePassed;

    public bool levelFinished;

    void Start()
    {
        stopwatch = new Stopwatch();
    }

    void Update()
    {
        timePassed = string.Format("{0:D2}:{1:D2}.{2:D3}", stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);

        stopwatchText.text = timePassed;

        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (levelFinished)
        {
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
