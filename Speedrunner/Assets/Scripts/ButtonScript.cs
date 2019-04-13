using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public TMP_Text sensitivityAmount;
    public GameObject sensitivitySlider;
    public GameManagement gameManager;
    public GameObject inGameSettingsMenu;
    public GameObject pauseMenu;
    public PlayerMovement movementScript;
    public GameObject levelMenu;
    public GameObject musicPrefab;

    public float sensitivity;

    void Start()
    {
        if (transform.parent.name == "ButtonGrid" && transform.GetSiblingIndex() != 0)
        {
            if (PlayerPrefs.GetInt("level" + transform.GetSiblingIndex() + 1) == 0)
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + transform.GetSiblingIndex() + 1);

        if (GameObject.Find("Music") == null)
        {
            var music = Instantiate(musicPrefab, Vector3.zero, Quaternion.identity);

            music.name = "Music";
        }
    }

    public void OpenLevelMenu()
    {
        levelMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Settings()
    {
        if (PlayerPrefs.GetInt("sensitivity") != 0)
        {
            sensitivitySlider.GetComponent<Slider>().value = PlayerPrefs.GetInt("sensitivity");

            sensitivityAmount.text = PlayerPrefs.GetInt("sensitivity").ToString();
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }

        else
        {
            inGameSettingsMenu.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("scene_menu");

        if (GameObject.Find("Music") != null)
        {
            Destroy(GameObject.Find("Music"));
        }
    }

    public void Accept()
    {
        PlayerPrefs.SetInt("sensitivity", (int)sensitivitySlider.GetComponent<ButtonScript>().sensitivity);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }

        else
        {
            inGameSettingsMenu.SetActive(false);
            pauseMenu.SetActive(true);

            movementScript.sensitivity = PlayerPrefs.GetInt("sensitivity");
        }
    }

    public void SetSensitivity()
    {
        sensitivity = gameObject.GetComponent<Slider>().value;

        sensitivityAmount.text = sensitivity.ToString();
    }

    public void Resume()
    {
        gameManager.TogglePauseMenu();
    }

    public void InGameSettings()
    {
        inGameSettingsMenu.SetActive(true);
    }
}
