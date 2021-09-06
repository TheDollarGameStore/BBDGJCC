using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyButton : MonoBehaviour
{
    public Constants.Towers towerType;
    private int price;
    public TextMeshProUGUI priceText;
    // Start is called before the first frame update
    void Start()
    {
        switch (towerType)
        {
            case Constants.Towers.Brocolli:
                price = Constants.brocolliPrice;
                break;
            case Constants.Towers.Garlic:
                price = Constants.garlicPrice;
                break;
            case Constants.Towers.Tomato:
                price = Constants.tomatoPrice;
                break;
            case Constants.Towers.Turnip:
                price = Constants.turnipPrice;
                break;
        }

        priceText.text = price.ToString();
    }

    // Update is called once per frame
    public void Buy()
    {
        if (GameManager.instance.discipline >= price && CursorManager.instance.holding == Constants.Towers.None)
        {
            GameManager.instance.UpdateDiscipline(-price);
            CursorManager.instance.holding = towerType;
            GetComponent<AudioSource>().Play();
        }
    }
}
