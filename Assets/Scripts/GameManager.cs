using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject playField;

    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomEnemy();
    }

    private void SpawnRandomEnemy()
    {
        float enemyColumn = (float)(-2.6 * Random.Range(0, 6));

        float enemyZ = (float)(-3.5 + enemyColumn);

        Vector3 enemyPositionVector = new Vector3((float)14.5, (float)1.5, enemyZ);
        Instantiate(enemy, enemyPositionVector, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
