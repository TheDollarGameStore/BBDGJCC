using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance = null;

    // Start is called before the first frame update
    public GameObject tilePrefab;

    [HideInInspector]
    public GridTile[,] tiles;

    public int rows;
    public int columns;

    public List<GameObject> towerPrefabs;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        tiles = new GridTile[rows, columns];

        GenerateGrid();
    }

    // Update is called once per frame
    private void GenerateGrid()
    {
        int total = 0;
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                total++;
                GameObject newTile = Instantiate(tilePrefab, new Vector2((x - (columns / 2f) + 0.5f) * Constants.gridWidth, (y - (rows / 2f) + 0.5f) * Constants.gridHeight), Quaternion.identity);
                newTile.GetComponent<SpriteRenderer>().color = (total % 2) == 1 ? Constants.lightPink : Constants.darkPink;
                tiles[y, x] = newTile.GetComponent<GridTile>();
            }
        }
    }
}
