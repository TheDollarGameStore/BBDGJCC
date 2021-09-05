using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    // Start is called before the first frame update
    private float x;
    private float initialY;

    public float speed;
    public float amplitude;

    void Start()
    {
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (x < Mathf.PI)
        {
            x += speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, initialY + (Mathf.Sin(x) * amplitude), transform.position.z);
        }

        if (x >= Mathf.PI)
        {
            x = Mathf.PI;
            transform.position = new Vector3(transform.position.x, initialY + (Mathf.Sin(x) * amplitude), transform.position.z);
            Destroy(GetComponent<Bounce>());
        }
        
    }
}
