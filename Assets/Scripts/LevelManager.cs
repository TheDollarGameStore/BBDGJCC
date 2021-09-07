using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    [HideInInspector]
    public List<GameObject> remainingEnemies;
    [HideInInspector]
    public bool usedOnlyTommyTina = true;

    private bool levelComplete = false;

    public int endlessEnemyBaseAmt;
    public int endlessEnemyIncreaseAmt;
    public int endlessGroupSizeMultiplier;

    public Constants.Enemies[] level1;
    public Constants.Enemies[] level2;
    public Constants.Enemies[] level3;
    public Constants.Enemies[] level4;
    public Constants.Enemies[] level5;
    public Constants.Enemies[] level6;


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
            case -1:
                PlayerPrefs.SetInt("endlessLevel", 0);
                enemySpawner.enemyList = GenerateEndlessModeList();
                break;
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
            case 6:
                enemySpawner.enemyList = level6;
                break;
        }
        enemySpawner.StartSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawner.instance.enemyStack.Count == 0 && remainingEnemies.Count == 0 && !levelComplete)
        {
            int currentLevel = PlayerPrefs.GetInt("currentLevel");
            int endlessLevel = PlayerPrefs.GetInt("endlessLevel");
            if (currentLevel > 0)
            {
                PlayerPrefs.SetInt("currentLevel", ++currentLevel);
                levelComplete = true;
            }
            else
            {
                PlayerPrefs.SetInt("endlessLevel", ++endlessLevel);

                EnemySpawner enemySpawner = EnemySpawner.instance;
                enemySpawner.enemyList = GenerateEndlessModeList();
                enemySpawner.endlessGroupSpawnSize = endlessLevel * endlessGroupSizeMultiplier;
                enemySpawner.StartSpawner();
            }

            HandleAchievements(currentLevel, endlessLevel);

            if (currentLevel > Constants.maxLevel)
            {
                PlayerPrefs.SetInt("currentLevel", 1);
                StartCoroutine(LoadScene(0));
            }
            else if (currentLevel > 0)
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

    private Constants.Enemies[] GenerateEndlessModeList()
    {
        int endlessLevel = PlayerPrefs.GetInt("endlessLevel");
        int enemyListSize = endlessEnemyBaseAmt + (endlessEnemyIncreaseAmt * endlessLevel);
        Constants.Enemies[] enemyList = new Constants.Enemies[enemyListSize];

        for (int i = 0; i < enemyListSize; i++)
        {

            enemyList[i] = (Constants.Enemies)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Constants.Enemies)).Length);
        }

        return enemyList;
    }

    private void HandleAchievements(int currentLevel, int endlessLevel)
    {
        if (currentLevel > Constants.maxLevel)
        {
            PlayerPrefs.SetInt("earnedHowManyAreThere", 1);
            PlayerPrefs.SetInt("unlockedCabbitsu", 1);
        }

        if(usedOnlyTommyTina)
        {
            PlayerPrefs.SetInt("earnedLaTomatina", 1);
        }

        switch (currentLevel)
        {
            case -1:
                switch (endlessLevel)
                {
                    case 1:
                        PlayerPrefs.SetInt("earnedOneForTheMoney", 1);
                        break;
                    case 2:
                        PlayerPrefs.SetInt("earnedTwoForTheShow", 1);
                        break;
                    case 3:
                        PlayerPrefs.SetInt("earnedThreeToGetReady", 1);
                        break;
                    case 9:
                        PlayerPrefs.SetInt("earnedGoCatGo", 1);
                        break;
                }
                break;
            case 2:
                PlayerPrefs.SetInt("unlockedTina", 1);
                break;
            case 3:
                PlayerPrefs.SetInt("unlockedGary", 1);
                break;
            case 4:
                PlayerPrefs.SetInt("unlockedSteve", 1);
                break;
            case 5:
                PlayerPrefs.SetInt("unlockedTommy", 1);
                PlayerPrefs.SetInt("earnedGettingYourGreens", 1);
                break;
            case 6:
                PlayerPrefs.SetInt("unlockedIda", 1);
                PlayerPrefs.SetInt("earnedTheDevilsYouKnow", 1);
                break;
        }
    }
}
