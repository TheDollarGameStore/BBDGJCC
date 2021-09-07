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

    private bool frozen;


    // Start is called before the first frame update
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentDamage = damage;
        wobbler = GetComponent<Wobble>();
    }

    public void Freeze(float duaration)
    {
        CancelInvoke("Unfreeze");
        frozen = true;
        Invoke("Unfreeze", duaration);
    }

    private void Unfreeze()
    {
        frozen = false;
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
            float speed = moveSpeedSquaresPerSecond * Time.deltaTime / (frozen ? 2f : 1f);
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
        if (frozen)
        {
            sr.color = Color.Lerp(sr.color, Constants.frozen, 5f * Time.deltaTime);
        }
        else
        {
            sr.color = Color.Lerp(sr.color, Constants.white, 5f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            sr.color = Constants.damage;
            IProjectile projectile = collision.gameObject.GetComponent<IProjectile>();

            health -= projectile.damage;

            if (projectile.ccTimer != 0)
            {
                Freeze(projectile.ccTimer);
            }

            if (projectile.speed != 0)
            {
                Destroy(collision.gameObject);
            }

            if (health <= 0)
            {
                LevelManager.instance.remainingEnemies.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            sr.color = Constants.damage;
            IProjectile projectile = collision.gameObject.GetComponent<IProjectile>();

            health -= projectile.damage;

            if (projectile.ccTimer != 0)
            {
                Freeze(projectile.ccTimer);
            }

            if (projectile.speed != 0)
            {
                Destroy(collision.gameObject);
            }

            if (health <= 0)
            {
                LevelManager.instance.remainingEnemies.Remove(gameObject);
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
