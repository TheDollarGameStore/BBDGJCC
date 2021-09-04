using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Slider loadingSlider;

    public void PlayGame()
    {
        StartCoroutine(LoadScene(1));
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
