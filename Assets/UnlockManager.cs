using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockManager : MonoBehaviour
{
    public GameObject unlockPrefab;

    public bool goodUnlock;

    public GameObject greenBack;
    public GameObject redBack;
    public GameObject goodText;
    public GameObject badText;

    public GameObject brogle;
    public GameObject patrick;
    public GameObject tina;
    public GameObject steve;
    public GameObject gary;
    public GameObject tommy;
    public GameObject ida;

    private AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();

        initializeUnlockPrefab();

        if (goodUnlock)
        {
            audioSources[1].Play();
            greenBack.SetActive(true);
            goodText.SetActive(true);
        }
        else
        {
            audioSources[0].Play();
            redBack.SetActive(true);
            badText.SetActive(true);
        }
        Invoke("ShowUnlock", 2f);
    }

    private void initializeUnlockPrefab()
    {
        System.Enum unlockEnum = LevelManager.instance.unlockList.Dequeue();

        switch (unlockEnum)
        {
            case Constants.Enemies.Burger:
                unlockPrefab = patrick;
                goodUnlock = false;
                break;
            case Constants.Enemies.Doughnut:
                unlockPrefab = steve;
                goodUnlock = false;
                break;
            case Constants.Enemies.IceCream:
                unlockPrefab = ida;
                goodUnlock = false;
                break;
            case Constants.Towers.Brocolli:
                unlockPrefab = brogle;
                goodUnlock = true;
                break;
            case Constants.Towers.Turnip:
                unlockPrefab = tina;
                goodUnlock = true;
                break;
            case Constants.Towers.Garlic:
                unlockPrefab = gary;
                goodUnlock = true;
                break;
            case Constants.Towers.Tomato:
                unlockPrefab = tommy;
                goodUnlock = true;
                break;
        }
    }

    // Update is called once per frame
    void ShowUnlock()
    {
        if (!goodUnlock)
        {
            Instantiate(unlockPrefab, new Vector3(325, -80, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(unlockPrefab, new Vector3(375, 80, 0), Quaternion.identity);
        }

        Invoke("LoadNextScene", 8f);
    }

    void LoadNextScene()
    {
        if (LevelManager.instance.unlockList.Count != 0)
        {
            StartCoroutine(LoadScene(2));
        }
        else
        {
            StartCoroutine(LoadScene(1));
        }
    }

    private IEnumerator LoadScene(int buildIndex)
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(buildIndex);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress == 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
