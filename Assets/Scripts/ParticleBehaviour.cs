using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Sprite> sprites;

    private SpriteRenderer sr;

    private Vector2 dir;
    public float startSpeed;
    private float speed;
    private float alpha;

    public float lifetime;

    public Color particleColor;

    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        sr.color = particleColor;
        speed = startSpeed;
        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        alpha = 1f;

        sr.sprite = sprites[Random.Range(0, sprites.Count)];

        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
    }

    // Update is called once per frame
    void Update()
    {
        speed -= (startSpeed / lifetime) * Time.deltaTime;
        alpha -= (1f / lifetime) * Time.deltaTime;

        transform.position += (Vector3)dir * speed * Time.deltaTime;

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

        if (alpha <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
