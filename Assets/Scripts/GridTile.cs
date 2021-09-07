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
            case Constants.Towers.Brocolli:
                tower = Instantiate(GridManager.instance.towerPrefabs[(int)Constants.Towers.Brocolli - 1], transform.position + (Vector3)(Vector2.up * Constants.gridHeight / 6f), Quaternion.identity).GetComponent<ITower>();
                LevelManager.instance.usedOnlyTommyTina = false;
                break;
            case Constants.Towers.Garlic:
                tower = Instantiate(GridManager.instance.towerPrefabs[(int)Constants.Towers.Garlic - 1], transform.position + (Vector3)(Vector2.up * Constants.gridHeight / 6f), Quaternion.identity).GetComponent<ITower>();
                LevelManager.instance.usedOnlyTommyTina = false;
                break;
            case Constants.Towers.Tomato:
                tower = Instantiate(GridManager.instance.towerPrefabs[(int)Constants.Towers.Tomato - 1], transform.position + (Vector3)(Vector2.up * Constants.gridHeight / 6f), Quaternion.identity).GetComponent<ITower>();
                break;
            case Constants.Towers.Turnip:
                tower = Instantiate(GridManager.instance.towerPrefabs[(int)Constants.Towers.Turnip - 1], transform.position + (Vector3)(Vector2.up * Constants.gridHeight / 6f), Quaternion.identity).GetComponent<ITower>();
                break;
        }

        GetComponent<AudioSource>().Play();

        return true;
    }
}
