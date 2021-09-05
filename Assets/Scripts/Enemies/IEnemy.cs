using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : MonoBehaviour
{
    public Constants.Enemies enemyType;
    public int health;
    public int damage;
    public float moveSpeedSquaresPerSecond;


    [HideInInspector]
    public int row;
    [HideInInspector]
    public int col;

    private SpriteRenderer sr;
    private float timePassed = 0;
    protected GridTile targetTile = null;
    private Vector3 targetPosition;
    private float currentDamage;
    private Wobble wobbler;


    // Start is called before the first frame update
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentDamage = damage;
        wobbler = GetComponent<Wobble>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (!wobbler.isWobbling)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= 1)
            {
                timePassed = 0;
            }
            Move();
            DoDamage();
            HandleColors();
        }
    }

    public void Move()
    {
        if (col > 0)
        {
            float speed = moveSpeedSquaresPerSecond * Time.deltaTime;
            if (targetTile == null)
            {
                targetTile = GridManager.instance.tiles[row, col - 1];
                targetPosition = targetTile.transform.position + (Vector3)(Vector2.up * Constants.gridHeight / 6f) + (Vector3)(Vector2.left * (Constants.gridWidth / 6f + 0.5f));
            }

            if (targetTile.tower == null)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
                currentDamage = damage;
            }

            if (Vector3.Distance(transform.position, targetPosition) < 15f)
            {
                col -= 1;
                targetTile = null;
            }
        }
        else
        {
            GridTile gridTile = GridManager.instance.tiles[row, 0];
            transform.position = gridTile.transform.position + (Vector3)(Vector2.up * Constants.gridHeight / 6f);
        }
    }

    private void HandleColors()
    {
        sr.color = Color.Lerp(sr.color, Constants.white, 5f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            sr.color = Constants.damage;
            IProjectile projectile = collision.gameObject.GetComponent<IProjectile>();

            health -= projectile.damage;

            Destroy(collision.gameObject);

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    protected void DoDamage()
    {
        if (targetTile != null && targetTile.tower != null)
        {
            currentDamage += damage * Time.deltaTime;

            if(currentDamage > damage)
            {
                targetTile.tower.TakeDamage(damage);
                wobbler.DoTheWobble();
                currentDamage = 0;
            }
        }
    }
}
