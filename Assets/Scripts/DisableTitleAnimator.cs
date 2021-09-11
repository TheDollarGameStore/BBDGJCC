using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTitleAnimator : MonoBehaviour
{

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("DisableAnim");
    }

    IEnumerator DisableAnim() 
    {
        yield return new WaitForSeconds(2.0f);
        anim.enabled = false;
    }

}
