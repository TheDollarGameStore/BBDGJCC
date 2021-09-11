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
    private SpriteRenderer[] srs;
    private float timePassed = 0;
    protected GridTile targetTile = null;
    private Vector3 targetPosition;
    private float currentDamage;
    private Wobble wobbler;

    private Animator anim;

    private bool frozen;

    private AudioSource audioSource;

    public GameObject particle;
    public GameObject enemyDieSound;


    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        srs = GetComponentsInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        currentDamage = damage;
        wobbler = GetComponent<Wobble>();
    }

    public void Freeze(float duaration)
    {
        CancelInvoke("Unfreeze");
        frozen = true;
        anim.speed = 0.5f;
        Invoke("Unfreeze", duaration);
    }

    private void Unfreeze()
    {
        frozen = false;
        anim.speed = 1;
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

    public void PlayRandomize()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
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

            LevelMenuScript.instance.EndGame(false);
        }
    }

    private void HandleColors()
    {
        if (frozen)
        {
            sr.color = Color.Lerp(sr.color, Constants.frozen, 5f * Time.deltaTime);
            foreach (SpriteRenderer sri in srs)
            {
                sri.color = Color.Lerp(sr.color, Constants.frozen, 5f * Time.deltaTime);
            }
        }
        else
        {
            sr.color = Color.Lerp(sr.color, Constants.white, 5f * Time.deltaTime);
            foreach (SpriteRenderer sri in srs)
            {
                sri.color = Color.Lerp(sr.color, Constants.white, 5f * Time.deltaTime);
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
                if (anim.GetBool("isAttacking") == false)
                {
                    anim.SetBool("isAttacking", true);
                }
                targetTile.tower.TakeDamage(damage);
                wobbler.DoTheWobble();
                currentDamage = 0;
            }
        }
        else
        {
            if (anim.GetBool("isAttacking") == true)
            {
                anim.SetBool("isAttacking", false);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        PlayRandomize();
        sr.color = Constants.damage;
        foreach (SpriteRenderer sri in srs)
        {
            sri.color = Constants.damage;
        }

        health -= damage;

        if (health <= 0)
        {
            LevelManager.instance.remainingEnemies.Remove(gameObject);
            for (int i = 0; i < 8; i++)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
            }
            Instantiate(enemyDieSound, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            PlayRandomize();
            sr.color = Constants.damage;
            foreach (SpriteRenderer sri in srs)
            {
                sri.color = Constants.damage;
            }
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
}
