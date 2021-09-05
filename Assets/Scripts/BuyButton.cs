using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyButton : MonoBehaviour
{
    public Constants.Towers towerType;
    public int price;
    public TextMeshProUGUI priceText;
    // Start is called before the first frame update
    void Start()
    {
        priceText.text = price.ToString();
    }

    // Update is called once per frame
    public void Buy()
    {
        if (GameManager.instance.discipline >= price && CursorManager.instance.holding == Constants.Towers.None)
        {
            GameManager.instance.UpdateDiscipline(-price);
            CursorManager.instance.holding = towerType;
        }
    }
}
