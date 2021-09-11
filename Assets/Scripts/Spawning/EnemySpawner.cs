using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector]
    public List<SpawningContainer> enemyList;


    public static EnemySpawner instance;
    [HideInInspector]
    public Queue<SpawningContainer> enemyQueue = new Queue<SpawningContainer>();
    [HideInInspector]
    public int unspawnedEnemies = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void StartSpawner()
    {
        foreach (SpawningContainer enemy in enemyList)
        {
            enemyQueue.Enqueue(enemy);
        }
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Spawner()
    {
        while (enemyQueue.Count != 0)
        {
            unspawnedEnemies++;
            SpawningContainer spawningContainer = enemyQueue.Dequeue();

            yield
            return new WaitForSeconds(spawningContainer.waitTime);

            PlaceRandomEnemy(spawningContainer.enemyType);
        }
    }

    private void PlaceRandomEnemy(Constants.Enemies nextEnemy)
    {
        GridManager gridManager = GridManager.instance;

        if (gridManager.tiles != null)
        {
            int enemyRow = Random.Range(0, gridManager.rows);
            int enemyColumn = gridManager.columns;

            GridTile enemyTile = gridManager.tiles[enemyRow, enemyColumn-1];

            GameObject newEnemy = Instantiate(gridManager.enemyPrefabs[(int)nextEnemy], enemyTile.transform.position + (Vector3)(Vector2.up * Constants.gridHeight / 6f), Quaternion.identity);
            IEnemy enemy = newEnemy.GetComponent<IEnemy>();
            enemy.row = enemyRow;
            enemy.col = enemyColumn;
            LevelManager.instance.remainingEnemies.Add(newEnemy);

            Slider satisfactionSlider = GameManager.instance.satisfactionSlider.GetComponent<Slider>();
            satisfactionSlider.value += 1;
            unspawnedEnemies--;

        }
    }
}
