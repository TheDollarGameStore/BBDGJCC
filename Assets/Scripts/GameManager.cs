using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public TextMeshProUGUI disciplineCounter;

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
        UpdateDiscipline(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDiscipline(int change)
    {
        discipline += change;
        disciplineCounter.text = discipline.ToString();
    }
}
