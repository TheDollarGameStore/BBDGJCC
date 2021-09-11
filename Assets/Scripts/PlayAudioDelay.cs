using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioDelay : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaySound()
    {
        audioSource.Play();
    }
}
