using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LairOfEnemies : MonoBehaviour
{
    public int LairHitPoint;
    private int _maxLairHitPoint;
    public GameObject HealthBarPrefab;
    private HealthBar _healthBar;
    public float PositionHealthBar;

    public int Xsize = 3;
    public int Zsize = 3;

    private int _numberOfLair = 6;


    private void Start()
    {
        _maxLairHitPoint = LairHitPoint;
        GameObject healthBar = Instantiate(HealthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.SetHealthBarHeightAtGround(PositionHealthBar);
        _healthBar.Setup(transform);
        SetHealth(LairHitPoint, _maxLairHitPoint);
    }

    private void Update()
    {
        
    }

    public void GetDamage(int damageValue)
    {
        LairHitPoint -= damageValue;
        SetHealth(LairHitPoint, _maxLairHitPoint);
        if (LairHitPoint <= 0)
        {
            Destruct();
        }
    }

    private void Destruct()
    {
        _numberOfLair--;
        Destroy(gameObject, 1f);
        if (_healthBar)
        {
            _healthBar.DelHealthBar();
        }
        WinCheck();
    }

    public void SetHealth(int health, int maxHealth)
    {
        float xScale = (float)health / maxHealth;
        xScale = Mathf.Clamp01(xScale) * Xsize;
        _healthBar.SetHealthBar(xScale);
    }

     void WinCheck()
    {
        //Enemy[] aliveEnemies = FindObjectsOfType<Enemy>();
        //int numberAliveEnemies = aliveEnemies.Length;
        if (_numberOfLair == 0)
        {
            //Вызываем окно победы
        }
    }
}
