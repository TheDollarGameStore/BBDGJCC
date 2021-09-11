using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioDelay : MonoBehaviour
{
    public float delay;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("PlaySound", delay);
    }

    // Update is called once per frame
    void PlaySound()
    {
        audioSource.Play();
    }
}
