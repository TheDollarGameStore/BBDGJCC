using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector]
    public Constants.Enemies[] enemyList;


    public static EnemySpawner instance;
    public int initialWait;
    public int minTimeBetweenSpawns;
    public int maxTimeBetweenSpawns;
    [HideInInspector]
    public Queue<Constants.Enemies> enemyStack = new Queue<Constants.Enemies>();
    [HideInInspector]
    public int endlessGroupSpawnSize;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void StartSpawner()
    {
        foreach (Constants.Enemies enemy in enemyList)
        {
            enemyStack.Enqueue(enemy);
        }
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Spawner()
    {
        yield
        return new WaitForSeconds(initialWait);

        while (enemyStack.Count != 0)
        {
            for (int i = 0; i < endlessGroupSpawnSize; i++)
            {
                PlaceRandomEnemy();
            }
            yield
            return new WaitForSeconds(Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns + 1));
        }
    }

    private void PlaceRandomEnemy()
    {
        GridManager gridManager = GridManager.instance;

        if (gridManager.tiles != null)
        {
            int enemyRow = Random.Range(0, gridManager.rows);
            int enemyColumn = gridManager.columns - 1;

            GridTile enemyTile = gridManager.tiles[enemyRow, enemyColumn];


            if (enemyStack.Count > 0)
            {
                Constants.Enemies nextEnemy = enemyStack.Dequeue();
                GameObject newEnemy = Instantiate(gridManager.enemyPrefabs[(int)nextEnemy], enemyTile.transform.position + (Vector3)(Vector2.up * Constants.gridHeight / 6f), Quaternion.identity);
                IEnemy enemy = newEnemy.GetComponent<IEnemy>();
                enemy.row = enemyRow;
                enemy.col = enemyColumn;
                LevelManager.instance.remainingEnemies.Add(newEnemy);
            }

        }
    }
}
