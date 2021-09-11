using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    [HideInInspector]
    public List<GameObject> remainingEnemies = new List<GameObject>();
    [HideInInspector]
    public bool usedOnlyTommyTina = true;
    [HideInInspector]
    public Queue<Enum> unlockList = new Queue<Enum>();
    [HideInInspector]
    public bool hasUnlock = false;


    public int endlessEnemyBaseAmt;
    public int endlessEnemyIncreaseAmt;
    public int endlessGroupSizeMultiplier;
    public int endlessInitialWait;
    public int endlessMinTimeBetweenSpawns;
    public int endlessMaxTimeBetweenSpawns;

    public TextMeshProUGUI levelText;

    private List<SpawningContainer> spawningList = new List<SpawningContainer>();
    private int endlessGroupSpawnSize = 1;
    private bool levelComplete = false;
    private bool spawningComplete = false;


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
        Time.timeScale = 1;
        int currentLevel = PlayerPrefs.GetInt("currentLevel");

        if(currentLevel > 0)
        {
            levelText.text = "Level " + currentLevel;
        }

        if (!PlayerPrefs.HasKey("unlockedPatrick"))
        {
            PlayerPrefs.SetInt("unlockedCollie", 1);
            PlayerPrefs.SetInt("unlockedPatrick", 1);
            unlockList.Enqueue(Constants.Towers.Brocolli);
            unlockList.Enqueue(Constants.Enemies.Burger);
            SceneManager.LoadScene(2);
        }

        EnemySpawner enemySpawner = EnemySpawner.instance;
        switch (currentLevel)
        {
            case -1:
                PlayerPrefs.SetInt("endlessLevel", 0);
                levelText.text = "Endless " + (PlayerPrefs.GetInt("endlessLevel") + 1);
                GenerateEndlessModeList();
                break;
            case 1:
                GenerateLevel1();
                break;
            case 2:
                GenerateLevel2();
                break;
            case 3:
                GenerateLevel3();
                break;
            case 4:
                GenerateLevel4();
                break;
            case 5:
                GenerateLevel5();
                break;
            case 6:
                GenerateLevel6();
                break;
        }

        PrepareSpawningList();
        enemySpawner.enemyList = spawningList;

        Slider satisfactionSlider = GameManager.instance.satisfactionSlider.GetComponent<Slider>();
        satisfactionSlider.maxValue = spawningList.Count;
        satisfactionSlider.value = 0;

        enemySpawner.StartSpawner();
        spawningComplete = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawningComplete && !levelComplete && EnemySpawner.instance.enemyQueue.Count == 0 && remainingEnemies.Count == 0 && EnemySpawner.instance.unspawnedEnemies == 0)
        {
            int currentLevel = PlayerPrefs.GetInt("currentLevel");
            int endlessLevel = PlayerPrefs.GetInt("endlessLevel");
            if (currentLevel > 0)
            {
                PlayerPrefs.SetInt("currentLevel", ++currentLevel);
                levelComplete = true;
                LevelMenuScript.instance.EndGame(levelComplete);
            }
            else
            {
                spawningComplete = false;
                PlayerPrefs.SetInt("endlessLevel", ++endlessLevel);

                levelText.text = "Endless " + (PlayerPrefs.GetInt("endlessLevel") + 1);

                EnemySpawner enemySpawner = EnemySpawner.instance;
                endlessGroupSpawnSize = endlessLevel * endlessGroupSizeMultiplier;
                GenerateEndlessModeList();
                PrepareSpawningList();
                enemySpawner.enemyList = spawningList;

                Slider satisfactionSlider = GameManager.instance.satisfactionSlider.GetComponent<Slider>();
                satisfactionSlider.maxValue = spawningList.Count;
                satisfactionSlider.value = 0;

                enemySpawner.StartSpawner();
                spawningComplete = true;
            }
            HandleAchievements(currentLevel, endlessLevel);
        }
    }

    private void GenerateEndlessModeList()
    {
        int endlessLevel = PlayerPrefs.GetInt("endlessLevel");
        int enemyListSize = endlessEnemyBaseAmt + (endlessEnemyIncreaseAmt * endlessLevel);
        int counter = 0;
        spawningList = new List<SpawningContainer>();

        while (enemyListSize > 0)
        {
            int waitTime = endlessInitialWait + (counter * UnityEngine.Random.Range(endlessMinTimeBetweenSpawns, endlessMaxTimeBetweenSpawns));

            for (int i = 0; i < endlessGroupSpawnSize; i++)
            {
                if (enemyListSize > 0)
                {
                    spawningList.Add(new SpawningContainer((Constants.Enemies)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Constants.Enemies)).Length), waitTime));
                    enemyListSize--;
                }
            }
            counter++;
        }
    }

    private void HandleAchievements(int currentLevel, int endlessLevel)
    {
        if (currentLevel > Constants.maxLevel)
        {
            PlayerPrefs.SetInt("earnedHowManyAreThere", 1);
            PlayerPrefs.SetInt("unlockedCabbitsu", 1);
        }

        if (usedOnlyTommyTina)
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
                if (!PlayerPrefs.HasKey("unlockedTina"))
                {
                    PlayerPrefs.SetInt("unlockedTina", 1);
                    hasUnlock = true;
                    unlockList.Enqueue(Constants.Towers.Turnip);
                }
                break;
            case 3:
                if (!PlayerPrefs.HasKey("unlockedGary"))
                {
                    PlayerPrefs.SetInt("unlockedGary", 1);
                    hasUnlock = true;
                    unlockList.Enqueue(Constants.Towers.Garlic);
                }
                break;
            case 4:
                if (!PlayerPrefs.HasKey("unlockedSteve"))
                {
                    PlayerPrefs.SetInt("unlockedSteve", 1);
                    unlockList.Enqueue(Constants.Enemies.Doughnut);
                }
                hasUnlock = true;
                break;
            case 5:
                if (!PlayerPrefs.HasKey("unlockedTommy"))
                {
                    PlayerPrefs.SetInt("unlockedTommy", 1);
                    PlayerPrefs.SetInt("earnedGettingYourGreens", 1);
                    hasUnlock = true;
                    unlockList.Enqueue(Constants.Towers.Tomato);
                }
                break;
            case 6:
                if (!PlayerPrefs.HasKey("unlockedIda"))
                {
                    PlayerPrefs.SetInt("unlockedIda", 1);
                    PlayerPrefs.SetInt("earnedTheDevilsYouKnow", 1);
                    hasUnlock = true;
                    unlockList.Enqueue(Constants.Enemies.IceCream);
                }
                break;
        }
    }

    private void PrepareSpawningList()
    {
        int previousWaitTime = 0;

        for (int i = 0; i < spawningList.Count; i++)
        {
            int waitTime = spawningList[i].waitTime;
            spawningList[i].waitTime -= previousWaitTime;
            previousWaitTime = waitTime;
        }
    }

    private void addEnemy(Constants.Enemies enemyType, int enemyCount, int timeBefore, int timeBetween)
    {
        for(int i = 0; i < enemyCount; i++)
        {
            spawningList.Add(new SpawningContainer(enemyType, timeBefore + (i * timeBetween)));
        }
        spawningList.Sort();
    }

    private void GenerateLevel1()
    {
        // broccoli unlocked
        // burger unlocked
        // learn how to place broccoli
        // 15s
        addEnemy(Constants.Enemies.Burger, 3, 2, 4);
        addEnemy(Constants.Enemies.Burger, 1, 12, 0);
    }

    private void GenerateLevel2()
    {
        // turnip unlocked
        // learn how to manage economy
        // 60s
        addEnemy(Constants.Enemies.Burger, 5, 3, 6);
        addEnemy(Constants.Enemies.Burger, 30, 30, 1);
    }

    private void GenerateLevel3()
    {
        // garlic unlocked
        // fend off big waves any way you like
        // 90s
        addEnemy(Constants.Enemies.Burger, 5, 3, 6);
        addEnemy(Constants.Enemies.Burger, 60, 30, 1);
        addEnemy(Constants.Enemies.Burger, 30, 60, 1);
    }

    private void GenerateLevel4()
    {
        // doughnut unlocked
        // learn the power of garlic
        // 90s
        addEnemy(Constants.Enemies.Burger, 5, 3, 6);
        addEnemy(Constants.Enemies.Burger, 60, 30, 1);
        addEnemy(Constants.Enemies.Burger, 30, 60, 1);
        addEnemy(Constants.Enemies.Doughnut, 9, 45, 5);
    }

    private void GenerateLevel5()
    {
        // tomato unlocked
        // fend off big waves any way you like
        // 120s
        addEnemy(Constants.Enemies.Burger, 5, 3, 6);
        addEnemy(Constants.Enemies.Burger, 90, 30, 1);
        addEnemy(Constants.Enemies.Burger, 60, 60, 1);
        addEnemy(Constants.Enemies.Burger, 30, 90, 1);
        addEnemy(Constants.Enemies.Doughnut, 15, 45, 5);
        addEnemy(Constants.Enemies.Doughnut, 9, 75, 5);
    }

    private void GenerateLevel6()
    {
        // ice cream unlocked
        // learn the power of tomato
        // 120s
        addEnemy(Constants.Enemies.Burger, 5, 3, 6);
        addEnemy(Constants.Enemies.Burger, 90, 30, 1);
        addEnemy(Constants.Enemies.Burger, 60, 60, 1);
        addEnemy(Constants.Enemies.Burger, 30, 90, 1);
        addEnemy(Constants.Enemies.Doughnut, 15, 45, 5);
        addEnemy(Constants.Enemies.Doughnut, 9, 75, 5);
        addEnemy(Constants.Enemies.IceCream, 12, 60, 5);
        addEnemy(Constants.Enemies.IceCream, 30, 90, 1);
    }
}
