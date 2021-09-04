using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    public int initialWait;
    public int minTimeBetweenSpawns;
    public int maxTimeBetweenSpawns;
    public List<Constants.Enemies> enemyList;
    private Stack<Constants.Enemies> enemyStack = new Stack<Constants.Enemies>();

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
        foreach (Constants.Enemies enemy in enemyList)
        {
            enemyStack.Push(enemy);
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
            PlaceRandomEnemy();
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

            Constants.Enemies nextEnemy = enemyStack.Pop();

            Instantiate(gridManager.enemyPrefabs[(int)nextEnemy], enemyTile.transform.position + (Vector3)(Vector2.up * Constants.gridHeight / 6f), Quaternion.identity);
        }
    }
}