using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : MonoBehaviour
{
    public Constants.Enemies enemyType;
    public int health;
    public float moveSpeedSquaresPerSecond;
    public AnimationCurve speedCurve;

    private float leftColumnX;
    private float timePassed = 0;


    // Start is called before the first frame update
    private void Start()
    {
        leftColumnX = GridManager.instance.transform.position.x + (0 - (GridManager.instance.columns / 2f) + 0.5f) * Constants.gridWidth;
    }

    // Update is called once per frame
    public void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= 1)
        {
            timePassed = 0;
        }
        Move();
    }

    public void Move()
    {
        if (transform.position.x > leftColumnX)
        {
            float speed = speedCurve.Evaluate(timePassed) * Time.deltaTime;
            transform.position += Vector3.left * moveSpeedSquaresPerSecond * 1.9f * Constants.gridWidth * speed;
        }
    }
}
