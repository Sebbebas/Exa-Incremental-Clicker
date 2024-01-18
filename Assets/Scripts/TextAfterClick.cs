using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAfterClick : MonoBehaviour
{
    //Configurable Parameters
    [SerializeField] float aliveTime = 2;

    //Cached References
    TextMeshProUGUI objectText;
    Banana banana;

    void Start()
    {
        banana = FindObjectOfType<Banana>();
        objectText = GetComponentInChildren<TextMeshProUGUI>();

        objectText.text = "+" + banana.GetBananasOnClick().ToString();

        Destroy(gameObject, aliveTime);
    }
}
