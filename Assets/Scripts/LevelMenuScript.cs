using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelMenuScript : MonoBehaviour
{
    public static LevelMenuScript instance;

    public Slider loadingSlider;
    public GameObject hudGameObject;
    public GameObject pauseMenuGameObject;
    public GameObject endMenu;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TogglePause();
        }
    }

    public void MainMenu()
    {
        Transitioner.instance.FadeIn(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TogglePause()
    {
        hudGameObject.SetActive(!hudGameObject.activeSelf);
        pauseMenuGameObject.SetActive(!pauseMenuGameObject.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void ContinueRetry()
    {
        Time.timeScale = 1;
        int currentLevel = PlayerPrefs.GetInt("currentLevel");

        if (currentLevel > Constants.maxLevel)
        {
            PlayerPrefs.SetInt("currentLevel", 1);
            Transitioner.instance.FadeIn(0);
        }
        else if (currentLevel > 0)
        {
            if (LevelManager.instance.hasUnlock)
            {
                Transitioner.instance.FadeIn(2);
            }
            else
            {
                Transitioner.instance.FadeIn(1);
            }
        }
    }

    public void EndGame(bool levelComplete)
    {
        Time.timeScale = 0;

        if (levelComplete)
        {
            endMenu.transform.GetChild(1).gameObject.SetActive(false); //Sad Cabbitsu
            endMenu.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "CONGRATULATIONS!";
            endMenu.transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>().text = "CONTINUE";
        }
        else
        {
            endMenu.transform.GetChild(0).gameObject.SetActive(false); //Happy Cabbitsu
            endMenu.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "GAME OVER";
            endMenu.transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>().text = "RETRY";
        }
        hudGameObject.SetActive(false);
        endMenu.SetActive(true);
    }
}
