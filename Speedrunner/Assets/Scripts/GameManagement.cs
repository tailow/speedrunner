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
    public long timePassedTicks;

    public bool levelFinished;

    public GameObject pauseMenu;
    public GameObject inGameSettingsMenu;

    void Start()
    {
        stopwatch = new Stopwatch();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        timePassed = string.Format("{0:D2}:{1:D2}.{2:D3}", stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
        timePassedTicks = stopwatch.ElapsedTicks;

        stopwatchText.text = timePassed;

        if (Input.GetButtonDown("Restart"))
        {
            Time.timeScale = 1;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (levelFinished)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Time.timeScale = 1;

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (Input.GetButtonDown("Exit"))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (inGameSettingsMenu.activeInHierarchy)
        {
            inGameSettingsMenu.SetActive(false);

            stopwatch.Start();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }

        else if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);

            stopwatch.Start();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }

        else
        {
            pauseMenu.SetActive(true);

            stopwatch.Stop();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
}
