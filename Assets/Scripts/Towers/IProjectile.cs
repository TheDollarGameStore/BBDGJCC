using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IProjectile : MonoBehaviour
{
    public int damage;
    public int speed;
    public float lifetime;
    public float ccTimer;
    public bool splashDamage;

    private bool dealtDamage = false;

    private void Start()
    {
        Invoke("Despawn", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(Vector2.right * speed * Time.deltaTime);

    }

    private void Despawn()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((!dealtDamage || splashDamage) && collision.gameObject.CompareTag("Enemy"))
        {
            dealtDamage = true;
            IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
            if (ccTimer != 0)
            {
                enemy.Freeze(ccTimer);
            }

            enemy.TakeDamage(damage);

            if (speed != 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
