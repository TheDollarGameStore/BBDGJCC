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
    public GameObject confirmMenu;

    private AudioSource[] audioSources;
    private string nextScreen;
    private string previousScreen;
    private bool gameOver = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
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
        ButtonSounds.instance.PlayClick();
        Transitioner.instance.FadeIn(0);
    }

    public void QuitGame()
    {
        ButtonSounds.instance.PlayClick();
        Application.Quit();
    }

    public void TogglePause()
    {
        ButtonSounds.instance.PlayClick();
        hudGameObject.SetActive(!hudGameObject.activeSelf);
        pauseMenuGameObject.SetActive(!pauseMenuGameObject.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void ContinueRetry()
    {
        ButtonSounds.instance.PlayClick();
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
        else
        {
            Transitioner.instance.FadeIn(1);
        }
    }

    public void EndGame(bool levelComplete)
    {
        if (!endMenu.activeSelf && !gameOver)
        {
            string endlessText = "";

            if(PlayerPrefs.GetInt("currentLevel") == -1)
            {
                int endlessLevel = PlayerPrefs.GetInt("endlessLevel");
                endlessText = "\r\nENDLESS " + (endlessLevel + 1);
            }

            if (levelComplete)
            {
                endMenu.transform.GetChild(1).gameObject.SetActive(false); //Sad Cabbitsu
                endMenu.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "CONGRATULATIONS!";
                endMenu.transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>().text = "CONTINUE";
                audioSources[0].Play();
            }
            else
            {
                endMenu.transform.GetChild(0).gameObject.SetActive(false); //Happy Cabbitsu
                endMenu.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "GAME OVER" + endlessText;
                endMenu.transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>().text = "RETRY";
                audioSources[1].Play();
            }
            hudGameObject.SetActive(false);
            endMenu.SetActive(true);
            pauseMenuGameObject.SetActive(false);
            gameOver = true;
        }
    }

    public void SetScreen(string screens)
    {
        ButtonSounds.instance.PlayClick();
        string[] screenArray = screens.Split(';');
        this.nextScreen = screenArray[0];
        this.previousScreen = screenArray[1];

        switch(nextScreen)
        {
            case "QUIT":
                hudGameObject.SetActive(false);
                endMenu.SetActive(false);
                confirmMenu.SetActive(true);
                pauseMenuGameObject.SetActive(false);
                break;
            case "MAINMENU":
                hudGameObject.SetActive(false);
                endMenu.SetActive(false);
                confirmMenu.SetActive(true);
                pauseMenuGameObject.SetActive(false);
                break;
        }
    }

    public void GoToNextScreen()
    {
        ButtonSounds.instance.PlayClick();
        Time.timeScale = 1;

        switch (nextScreen)
        {
            case "QUIT":
                QuitGame();
                confirmMenu.SetActive(false);
                break;
            case "MAINMENU":
                MainMenu();
                confirmMenu.SetActive(false);
                break;
        }
    }

    public void GoToPreviousScreen()
    {
        ButtonSounds.instance.PlayClick();
        switch (previousScreen)
        {
            case "PAUSE":
                hudGameObject.SetActive(false);
                endMenu.SetActive(false);
                confirmMenu.SetActive(false);
                pauseMenuGameObject.SetActive(true);
                break;
            case "END":
                hudGameObject.SetActive(false);
                endMenu.SetActive(true);
                confirmMenu.SetActive(false);
                pauseMenuGameObject.SetActive(false);
                break;
        }

    }
}
