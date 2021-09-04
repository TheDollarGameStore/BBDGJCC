using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    [HideInInspector]
    public ITower tower = null;
    // Start is called before the first frame update
    public bool PlaceTower(Constants.Towers newTower)
    {
        if (tower != null)
        {
            return false;
        }

        switch(newTower)
        {
            case Constants.Towers.Brogle:
                Instantiate(GridManager.instance.towerPrefabs[(int)Constants.Towers.Brogle - 1], transform.position + (Vector3)(Vector2.up * Constants.gridHeight / 6f), Quaternion.identity);
                break;
        }

        return true;
    }
}
