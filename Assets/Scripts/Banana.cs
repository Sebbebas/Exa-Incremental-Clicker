using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Banana : MonoBehaviour
{
    //Configurabel Perameters
    [Header("Banana Object")]
    [SerializeField] TextMeshProUGUI totalBananasText;
    [SerializeField, Tooltip("Currency")] double totalBananas;

    [Space]

    [SerializeField, Tooltip("How many bananas you have acumilated in total")] double lifeTimeBanana;

    [Header("On Click")]
    [SerializeField] GameObject spawnOnClick;
    [SerializeField, Tooltip("Currency / click")] int bananasOnClick = 1;

    [Header("Bananas per Second")]
    [SerializeField] TextMeshProUGUI bananasPerSecondText;
    [SerializeField] double bananasPerSecond;

    [Header("Banana UI Element")]
    [SerializeField] float onClickMultipiler = 1.35f;
    [SerializeField] float sizeTransitionSpeed;

    [Space]

    [SerializeField] Vector2 size;
    [SerializeField] float sizeMultiplier;

    //Private Variables
    private Vector2 sizeAtStart;

    //Chached Reference
    RectTransform bananaRectTransform;
    Shop shop;

    private void Start()
    {
        bananaRectTransform = GetComponent<RectTransform>();
        shop = FindObjectOfType<Shop>();
        sizeAtStart = bananaRectTransform.sizeDelta;
    }

    public void Update()
    {
        SetCurrentMaxSize();
    }

    public void SetCurrentMaxSize()
    {
        Vector2 currentMaxSize = sizeAtStart * sizeMultiplier;

        size = bananaRectTransform.sizeDelta;

        if (currentMaxSize.x > size.x && currentMaxSize.x > size.y && sizeMultiplier > 1)
        {
            bananaRectTransform.sizeDelta = Vector2.Lerp(size, new(size.x * sizeMultiplier, size.y * sizeMultiplier), Time.deltaTime * sizeTransitionSpeed);
        }
        else if (currentMaxSize.x < size.x && currentMaxSize.x < size.y && sizeMultiplier < 1)
        {
            bananaRectTransform.sizeDelta = Vector2.Lerp(size, new(size.x * sizeMultiplier, size.y * sizeMultiplier), Time.deltaTime * sizeTransitionSpeed);
        }
        else if (sizeMultiplier == 1)
        {
            bananaRectTransform.sizeDelta = Vector2.Lerp(size, new(sizeAtStart.x, sizeAtStart.y), Time.deltaTime * sizeTransitionSpeed);
        }
        else
        {
            bananaRectTransform.sizeDelta = currentMaxSize;
        }
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

    public void InstanisateOnMousePointer()
    {
        Instantiate(spawnOnClick, transform.position.normalized, Quaternion.identity);
    }

    #region Eventrigger
    public void Click()
    {
        AddBananas(bananasOnClick);
        AddLifeTimeBanana(bananasOnClick);
    }

    public void ChangeSize(float multiplier)
    {
        sizeMultiplier = multiplier;
    }
    #endregion

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
    public int GetBananasOnClick()
    {
        return bananasOnClick;
    }
    #endregion
}
