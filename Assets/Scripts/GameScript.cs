using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public Slider loadingSlider;
    public GameObject pauseMenuGameObject;

    public void MainMenu()
    {
        StartCoroutine(LoadScene(0));
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            pauseMenuGameObject.SetActive(!pauseMenuGameObject.activeSelf);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
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
