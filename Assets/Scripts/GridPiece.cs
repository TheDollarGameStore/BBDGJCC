using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece : MonoBehaviour
{
    [HideInInspector]
    public GameObject tower = null;

    public bool IsOccupied()
    {
        return tower != null;
    }
}
