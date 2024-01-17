using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum Type
{
    Cursor = 0,
    Grandma = 1,
    Farm = 2,
    Mine = 3,
    Factory = 4,
}

public class Shop : MonoBehaviour
{

    //Configurable Parameters
    [Header("Shop")]
    [SerializeField] Workers[] workers;

    
    //Private Variabels
    private float time;
    private double currentBPS;

    //15%
    private float procentegeIncresse = 1.15f;

    //Chaced Refrences
    Banana banana;

    [System.Serializable]
    public struct Workers
    {
        public Type type;
        public int total;

        [Space]

        public float startPrice;
        public float price;

        [Space]

        public float perSecond;
        public float multiplier;

        [Space]

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI priceText;
        public TextMeshProUGUI totalText;
    }

    private void Start()
    {
        banana = FindObjectOfType<Banana>();
    }

    private void Update()
    {
        AddBannanaEverySec();
        WorkersText();
    }

    private void AddBannanaEverySec()
    {
        CalculateBPS();

        if (time > 0) { time -= Time.deltaTime; return; }
        else
        {
            //Reset CountDown
            time = 1;
            
            CalculateBPS();

            //Add bananas
            banana.AddBananas(currentBPS);
            banana.AddLifeTimeBanana(currentBPS);
        }
    }

    private void CalculateBPS()
    {
        currentBPS = 0;

        foreach (var worker in workers)
        {
            currentBPS += worker.perSecond * worker.total * worker.multiplier;
        }
        System.Math.Round(currentBPS, 1);
        banana.UpdateText();
    }

    private void WorkersText()
    {
        foreach (var worker in workers)
        {
            //null Check
            if (worker.totalText == null && worker.totalText == null) { return; }

            //Make total nummber invisible if 0
            if (worker.total == 0) { worker.totalText.alpha = 0; }
            else { worker.totalText.alpha = 1; }

            //Shop Text
            worker.totalText.text = worker.total.ToString();
            worker.nameText.text = worker.type.ToString();

            //Price Text
            if (worker.price != 0) { worker.priceText.text = worker.price.ToString(); }
            else { worker.priceText.text = worker.startPrice.ToString(); }

            //Unlock?
            if (worker.startPrice > banana.GetLifeTimeBananas()) { Image parent = worker.nameText.GetComponentInParent<Image>(); }
        }
    }

    public void BuyWorker(int type)
    {
        //Price = StartingPrice
        float price = workers[type].startPrice;

        //Multiply price X ammount of times
        for (int i = 0; i < workers[type].total; i++)
        {
            price *= procentegeIncresse;
        }

        //nextupgrade would cost X
        float nextUpgradeCost = price * procentegeIncresse;
        nextUpgradeCost = Mathf.RoundToInt(nextUpgradeCost);

        //round and apply the price
        workers[type].price = Mathf.RoundToInt(price);

        if(Mathf.RoundToInt(price) <= banana.GetBananas()) { banana.RemoveBananas(price); workers[type].total++; workers[type].price = nextUpgradeCost; }
    }

    public double GetBPS()
    {
        return currentBPS;
    }
}
