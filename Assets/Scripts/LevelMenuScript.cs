using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelMenuScript : MonoBehaviour
{
    public Slider loadingSlider;
    public GameObject hudGameObject;
    public GameObject pauseMenuGameObject;

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TogglePause();
        }
    }

    public void MainMenu()
    {
        StartCoroutine(LoadScene(0));
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

    private IEnumerator LoadScene(int sceneBuildIndex)
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            loadingSlider.value = asyncOperation.progress;
            if (asyncOperation.progress == 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
