using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : MonoBehaviour
{
    public GameObject unlockPrefab;

    public bool goodUnlock;

    public GameObject greenBack;
    public GameObject redBack;
    public GameObject goodText;
    public GameObject badText;

    private AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();

        if (goodUnlock)
        {
            audioSources[1].Play();
            greenBack.SetActive(true);
            goodText.SetActive(true);
        }
        else
        {
            audioSources[0].Play();
            redBack.SetActive(true);
            badText.SetActive(true);
        }
        Invoke("ShowUnlock", 2f);
    }

    // Update is called once per frame
    void ShowUnlock()
    {
        if (!goodUnlock)
        {
            Instantiate(unlockPrefab, new Vector3(325, -80, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(unlockPrefab, new Vector3(375, 80, 0), Quaternion.identity);
        }
        
    }
}
