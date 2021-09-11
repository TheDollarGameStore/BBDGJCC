using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieSound : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("DestroySelf", 1f);

        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }

    // Update is called once per frame
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
