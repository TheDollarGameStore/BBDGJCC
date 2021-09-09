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
        price = Constants.GetTowerPrice(towerType);

        priceText.text = price.ToString();

        switch(towerType)
        {
            case Constants.Towers.Garlic:
                gameObject.SetActive(PlayerPrefs.HasKey("unlockedGary"));
                break;
            case Constants.Towers.Tomato:
                gameObject.SetActive(PlayerPrefs.HasKey("unlockedTommy"));
                break;
            case Constants.Towers.Turnip:
                gameObject.SetActive(PlayerPrefs.HasKey("unlockedTina"));
                break;
        }
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
