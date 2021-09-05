using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUnlock : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 targetScale;
    public float shrinkSpeed;
    public GameObject screenShake;

    private bool shook;

    void Awake()
    {
        targetScale = transform.localScale;
        transform.localScale = targetScale * 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x > targetScale.x)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
        }
        else
        {
            if (!shook)
            {
                shook = true;
                Instantiate(screenShake, transform.position, Quaternion.identity);
            }

            transform.localScale = targetScale;
        }
    }
}
