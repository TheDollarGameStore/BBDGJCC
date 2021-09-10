using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    // Start is called before the first frame update
    public float intensity;
    public float fade;

    // Update is called once per frame
    void FixedUpdate()
    {
        Camera.main.transform.position += new Vector3(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity), 0f);
        intensity -= fade;

        if (intensity <= 0)
        {
            Destroy(gameObject);
        }
    }
}
