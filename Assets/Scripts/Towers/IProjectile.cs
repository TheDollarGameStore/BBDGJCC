using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IProjectile : MonoBehaviour
{
    public int damage;
    public int speed;
    public float lifetime;

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
}
