using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUnlock : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 originalPos;
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, originalPos, 10f * Time.deltaTime);
    }
}
