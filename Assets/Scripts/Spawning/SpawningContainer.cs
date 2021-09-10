using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningContainer : IComparable<SpawningContainer>
{
    public Constants.Enemies enemyType;
    public int waitTime;

    public SpawningContainer(Constants.Enemies enemyType, int waitTime)
    {
        this.enemyType = enemyType;
        this.waitTime = waitTime;
    }

    public int CompareTo(SpawningContainer other)
    {
        return waitTime.CompareTo(other.waitTime);
    }
}
