using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doughnut : IEnemy
{
    // Update is called once per frame
    new void Update()
    {
        if (!GetComponent<Wobble>().isWobbling)
        {
            base.Update();
        }
    }
}
