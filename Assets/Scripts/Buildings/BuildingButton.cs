using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    public BuildingPlacer BuildingPlacer;
    public GameObject BuildingPrefab;
    public Text PriceText;

    private void Start()
    {
        PriceText.text = BuildingPrefab.GetComponent<Building>().Price.ToString();
    }

    public void TryBuy()
    {
        int price = BuildingPrefab.GetComponent<Building>().Price;
        Resources resources = FindObjectOfType<Resources>();
        if (resources.TryBuyBuilding(price))
        {            
            BuildingPlacer.CreateBuilding(BuildingPrefab);         
        }
        else
        {
            Debug.Log("We need more Gold!");
        }
    }
}
