using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInRT : MonoBehaviour
{
    public Vector3 offset;

    private bool sliding = false;

    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.localPosition += offset;
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
            rt.localPosition = Vector3.Lerp(rt.localPosition, Vector3.zero, 15f * Time.deltaTime);
        }
        
    }
}
