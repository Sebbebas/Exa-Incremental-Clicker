using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAfterClick : MonoBehaviour
{
    //Configurable Parameters
    [SerializeField] float aliveTime = 2;
    [SerializeField] float textGravity = 5;

    //Cached References
    TextMeshProUGUI objectText;
    Banana banana;

    void Start()
    {
        banana = FindObjectOfType<Banana>();
        objectText = GetComponentInChildren<TextMeshProUGUI>();

        objectText.text = "+" + banana.GetBananasOnClick().ToString();

        Vector2 mousePos = Input.mousePosition;
        objectText.rectTransform.position = mousePos;

        Destroy(gameObject, aliveTime);
    }

    private void Update()
    {
        objectText.rectTransform.position = new(objectText.rectTransform.position.x, objectText.rectTransform.position.y + textGravity);
        objectText.alpha -= Time.deltaTime;
    }
}
