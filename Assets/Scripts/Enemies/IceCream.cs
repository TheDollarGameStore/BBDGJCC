using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : IEnemy
{
    // Update is called once per frame
    new void Update()
    {
        base.Update();
        DoDamage();
    }

    new void DoDamage()
    {
        if (targetTile != null && targetTile.tower != null)
        {
            targetTile.tower.Freeze();
        }
    }
}
