using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    [HideInInspector]
    public List<GameObject> remainingEnemies;

    private bool levelComplete = false;


    public Constants.Enemies[] level1;
    public Constants.Enemies[] level2;
    public Constants.Enemies[] level3;
    public Constants.Enemies[] level4;
    public Constants.Enemies[] level5;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawner enemySpawner = EnemySpawner.instance;
        switch (PlayerPrefs.GetInt("currentLevel"))
        {
            case 1:
                enemySpawner.enemyList = level1;
                break;
            case 2:
                enemySpawner.enemyList = level2;
                break;
            case 3:
                enemySpawner.enemyList = level3;
                break;
            case 4:
                enemySpawner.enemyList = level4;
                break;
            case 5:
                enemySpawner.enemyList = level5;
                break;
        }
        enemySpawner.StartSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemySpawner.instance.enemyStack.Count == 0 && remainingEnemies.Count == 0 && !levelComplete)
        {
            int currentLevel = PlayerPrefs.GetInt("currentLevel");
            PlayerPrefs.SetInt("currentLevel", ++currentLevel);
            levelComplete = true;

            if (currentLevel > 5)
            {
                PlayerPrefs.SetInt("earnedHowManyAreThere", 1);
                PlayerPrefs.SetInt("currentLevel", 1);
                StartCoroutine(LoadScene(0));
            }
            else
            {
                StartCoroutine(LoadScene(1));
            }
        }
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress == 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
        levelComplete = false;
    }
}
