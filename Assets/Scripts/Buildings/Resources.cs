using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    private int _money = 100;
    private int _recruits = 100;

    public Text MoneyText;
    public Text RecruitsText;


    private void Start()
    {
        UpdateMoneyText();
        UpdateRecruitsText();
    }

    public bool TryBuyBuilding(int cost)
    {
        if (_money > cost)
        {
            _money -= cost;
            UpdateMoneyText();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryBuyUnit(int cost)
    {
        if (_recruits > cost)
        {
            _recruits -= cost;
            UpdateRecruitsText();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddRecruits(int volume)
    {
        _recruits += volume;
        UpdateRecruitsText();
    }

    public void AddMoney(int volume)
    {
        _money += volume;
        UpdateMoneyText();
    }
    private void UpdateMoneyText()
    {
        MoneyText.text = _money.ToString();
    }
    private void UpdateRecruitsText()
    {
        RecruitsText.text = _recruits.ToString();
    }
}
