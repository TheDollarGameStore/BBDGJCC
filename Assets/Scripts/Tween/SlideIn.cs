using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideIn : MonoBehaviour
{
    public Vector3 offset;

    private Vector3 originalPos;
    private bool sliding = false;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        transform.position += offset;
        Invoke("StartSlide", 1f);
    }

    void StartSlide()
    {
        sliding = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sliding)
        {
            transform.position = Vector3.Lerp(transform.position, originalPos, 15f * Time.deltaTime);
        }
        
    }
}
