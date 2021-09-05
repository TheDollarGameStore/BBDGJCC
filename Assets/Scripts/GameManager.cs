using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public TextMeshProUGUI disciplineCounter;

    private int disciplineShow;

    public int discipline;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        disciplineShow = discipline;
        disciplineCounter.text = disciplineShow.ToString();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (disciplineShow < discipline)
        {
            disciplineShow+= 5;

            disciplineCounter.text = disciplineShow.ToString();
        }

        if (disciplineShow > discipline)
        {
            disciplineShow -= 5;

            disciplineCounter.text = disciplineShow.ToString();
        }
    }

    public void UpdateDiscipline(int change)
    {
        discipline += change;
    }
}
