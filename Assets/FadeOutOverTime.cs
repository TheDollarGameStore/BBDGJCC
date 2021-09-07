using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutOverTime : MonoBehaviour
{
    private float alpha;
    public float lifetime;
    private float fadeSpeed;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        alpha = 1f;
        fadeSpeed = alpha / lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        alpha -= fadeSpeed * Time.deltaTime;

        sr.color = new Color(1f, 1f, 1f, alpha);
    }
}
