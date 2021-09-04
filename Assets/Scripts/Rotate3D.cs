using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate3D : MonoBehaviour
{
    public Vector3 direction;
    // Start is called before the first frame updat

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(direction * Time.deltaTime, Space.Self);
    }
}
