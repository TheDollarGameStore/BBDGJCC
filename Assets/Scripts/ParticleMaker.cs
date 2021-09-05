using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaker : MonoBehaviour
{
    public GameObject particlePrefab;

    public float spread;
    public float rate;
    public float initialDelay;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LaunchParticle", initialDelay);
    }

    // Update is called once per frame
    void LaunchParticle()
    {
        Invoke("LaunchParticle", rate);
        Instantiate(particlePrefab, transform.position + new Vector3(Random.Range(-spread, spread), 0f, 0f), particlePrefab.transform.rotation);
    }
}
