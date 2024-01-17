using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Banana : MonoBehaviour
{
    //Configurabel Perameters
    [Header("Bannana Object")]
    [SerializeField] TextMeshProUGUI totalBananasText;
    [SerializeField, Tooltip("Currency")] double totalBananas;
    [SerializeField, Tooltip("Currency / click")] int bananasOnClick = 1;

    [Space]

    [SerializeField, Tooltip("How many bananas you have acumilated in total")] double lifeTimeBanana;


    [Header("Bannanas per Second")]
    [SerializeField] TextMeshProUGUI bananasPerSecondText;
    [SerializeField] double bananasPerSecond;

    //Chached Reference
    Shop shop;

    private void Start()
    {
        shop = FindObjectOfType<Shop>();
    }

    public void Click()
    {
        AddBananas(bananasOnClick);
        AddLifeTimeBanana(bananasOnClick);
    }

    public void UpdateText()
    {
        //Round BANANAS to 1 decimal
        double originalValue = totalBananas;
        totalBananas = System.Math.Round(originalValue, 1);
        totalBananasText.text = totalBananas.ToString();

        //BANANAS Per Second
        bananasPerSecond = System.Math.Round(shop.GetBPS(), 1);
        bananasPerSecondText.text = bananasPerSecond.ToString() + "/s";
    }

    #region Change Values
    public void RemoveBananas(double remove)
    {
        totalBananas -= remove;
    }
    public void AddBananas(double add)
    {
        totalBananas += add;
    }
    public void AddLifeTimeBanana(double add)
    {
        lifeTimeBanana += add;
        lifeTimeBanana = System.Math.Round(lifeTimeBanana, 1);
    }
    #endregion

    #region Get Values
    public double GetBananas()
    {
        return totalBananas;
    }
    public double GetLifeTimeBananas()
    {
        return lifeTimeBanana;
    }
    #endregion
}
