using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    // Start is called before the first frame update
    public bool fadeIn;
    public int targetRoom;
    private float alpha;
    private Image sr;

    void Start()
    {
        sr = GetComponent<Image>();

        if (fadeIn)
        {
            alpha = 0f;
        }
        else
        {
            alpha = 1f;
        }


        sr.color = new Color(1f, 1f, 1f, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            if (alpha < 1f)
            {
                alpha += 1f * Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene(targetRoom);
            }
        }
        else
        {
            if (alpha > 0f)
            {
                alpha -= 1f * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        sr.color = new Color(1f, 1f, 1f, alpha);
    }
}
