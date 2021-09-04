using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : MonoBehaviour
{
    public Constants.Enemies enemyType;
    public int health;
    public float moveSpeedSquaresPerSecond;

    private float leftColumnX;

    // Start is called before the first frame update
    private void Start()
    {
        leftColumnX = GridManager.instance.transform.position.x + (0 - (GridManager.instance.columns / 2f) + 0.5f) * Constants.gridWidth;
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
    }

    public void Move()
    {
        if (transform.position.x > leftColumnX)
        {
            transform.position += Vector3.left * moveSpeedSquaresPerSecond * Constants.gridWidth * Time.deltaTime;
        }
    }
}
