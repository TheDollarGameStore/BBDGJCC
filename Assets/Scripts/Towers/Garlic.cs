using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garlic : ITower
{
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        base.HandleColors();
    }
}
