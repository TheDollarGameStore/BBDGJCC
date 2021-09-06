using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : ITower
{
    public GameObject soundObject;
    // Start is called before the first frame update
    private float shakiness;
    public float windUpSpeed;
    public float maxWind;

    public GameObject screenShake;

    private Vector3 startPos;
    void Start()
    {
        Instantiate(soundObject, transform.position, Quaternion.identity);
        startPos = transform.position;
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        base.HandleColors();
        shakiness += windUpSpeed * Time.deltaTime;

        if (shakiness >= maxWind)
        {
            transform.position = startPos;
            Shoot();
            Instantiate(screenShake, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        transform.position += (Vector3)new Vector2(Random.Range(-shakiness, shakiness), Random.Range(-shakiness, shakiness));
        transform.localScale += Vector3.one * Time.deltaTime * 0.05f;
        transform.position = Vector3.Lerp(transform.position, startPos, 10f * Time.deltaTime);
    }
}
