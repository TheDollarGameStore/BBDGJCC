using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(transform.right * 4000f * Time.deltaTime);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
