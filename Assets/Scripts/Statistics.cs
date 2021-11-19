using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    private int _numberOfDeadEnemies = 0;
    private int _numberOfDeadFriendlyUnits = 0;

    public Text EnemyStatisticText;
    public Text FriendlyUnitStatisticText;


    private void Start()
    {
        UpdateEnemyStatisticText();
        UpdateFriendlyUnitStatisticText();
    }

    public void AddFriendlyUnitDeath()
    {
        _numberOfDeadFriendlyUnits++;
        UpdateFriendlyUnitStatisticText();
    }

    public void AddEnemyDeath()
    {
        _numberOfDeadEnemies++;
        UpdateEnemyStatisticText();
    }
    private void UpdateEnemyStatisticText()
    {
        EnemyStatisticText.text = "Killed:" + _numberOfDeadEnemies.ToString();
    }
    private void UpdateFriendlyUnitStatisticText()
    {
        FriendlyUnitStatisticText.text = "Death:" + _numberOfDeadFriendlyUnits.ToString();
    }
}
