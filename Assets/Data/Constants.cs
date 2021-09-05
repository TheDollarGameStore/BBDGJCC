using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public static int gridWidth = 96;
    public static int gridHeight = 96;

    public static Color32 lightPink = new Color32(255, 96, 191, 255);
    public static Color32 darkPink = new Color32(253, 52, 155, 255);

    public static Color32 white = new Color32(255, 255, 255, 255);
    public static Color32 damage = new Color32(255, 80, 80, 255);
    public static Color32 frozen = new Color32(120, 220, 255, 255);

    public enum Towers {None, Brogle, Cabbage, Cherry, Tomato }

    public enum Enemies {Burger, Doughnut, IceCream  };
}
