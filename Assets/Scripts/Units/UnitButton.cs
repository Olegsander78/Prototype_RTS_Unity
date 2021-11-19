using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    public GameObject UnitPrefab;
    public Barack Barack;
    public Text PriceText;

    private void Start()
    {
        PriceText.text = UnitPrefab.GetComponent<Unit>().RecruitPrice.ToString();
    }
    public void TryBuy()
    {
        int recruitPrice = UnitPrefab.GetComponent<Unit>().RecruitPrice;
        Resources resources = FindObjectOfType<Resources>();
        if (resources.TryBuyUnit(recruitPrice))
        {            
            Barack.CreateUnit(UnitPrefab);            
        }
        else
        {
            Debug.Log("We need more Gold!");
        }
    }
}
