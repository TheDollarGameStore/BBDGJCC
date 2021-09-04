using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int height;
    public int width;

    private GameObject[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                tiles[y, x] = GameObject.Find((y + 1).ToString() + "_" + (x + 1).ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
