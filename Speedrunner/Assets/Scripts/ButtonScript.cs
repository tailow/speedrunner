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

    public float sensitivity;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        if (PlayerPrefs.GetFloat("sensitivity") != 0)
        {
            sensitivitySlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("sensitivity");

            sensitivityAmount.text = PlayerPrefs.GetFloat("sensitivity").ToString();
        }

        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("scene_menu");
    }

    public void Accept()
    {
        PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.GetComponent<ButtonScript>().sensitivity);

        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void SetSensitivity()
    {
        sensitivity = Mathf.Round(gameObject.GetComponent<Slider>().value * 100f) / 100f;

        sensitivityAmount.text = sensitivity.ToString();
    }
}
