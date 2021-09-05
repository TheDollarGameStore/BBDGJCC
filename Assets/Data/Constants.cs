using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public static int gridWidth = 96;
    public static int gridHeight = 96;

    public static Color32 lightPink = new Color32(238, 149, 191, 255);
    public static Color32 darkPink = new Color32(241, 132, 156, 255);

    public static Color32 white = new Color32(255, 255, 255, 255);
    public static Color32 damage = new Color32(255, 80, 80, 255);
    public static Color32 frozen = new Color32(120, 220, 255, 255);

    public static int brocolliPrice = 100;
    public static int garlicPrice = 200;
    public static int tomatoPrice = 150;
    public static int turnipPrice = 50;

    public enum Towers {None, Brocolli, Garlic, Tomato, Turnip }

    public enum Enemies {Burger, Doughnut, IceCream  };
}
