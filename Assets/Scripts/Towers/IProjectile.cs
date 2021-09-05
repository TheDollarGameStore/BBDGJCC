using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IProjectile : MonoBehaviour
{
    public int damage;
    public int speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(Vector2.right * speed * Time.deltaTime);
    }
}
