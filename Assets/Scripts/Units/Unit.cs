using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject
{
    public NavMeshAgent NavMeshAgent;
    public int RecruitPrice;
    public int UnitHealth;
    private int _maxUnitHealth;

    public GameObject HealthBarPrefab;
    private HealthBar _healthBar;


    public override void Start()
    {
        base.Start();
        _maxUnitHealth = UnitHealth;
        GameObject healthBar = Instantiate(HealthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(transform);
    }

    public override void WhenClickOnGround(Vector3 point)
    {
        base.WhenClickOnGround(point);
        NavMeshAgent.SetDestination(point);
    }

    public void TakeDamage(int damageValue)
    {
        UnitHealth -= damageValue;
        SetHealth(UnitHealth, _maxUnitHealth);
        if (UnitHealth <= 0)
        {
            Die(); 
        }
    }

    public virtual void Die()
    {
        FindObjectOfType<Statistics>().AddFriendlyUnitDeath();
        Destroy(gameObject,1f);
        FindObjectOfType<Management>().Unselect(this);
        if (_healthBar)
        {
            _healthBar.DelHealthBar();
        }        
    }

    public void SetHealth(int health, int maxHealth)
    {
        float xScale = (float)health / maxHealth;
        xScale = Mathf.Clamp01(xScale);
        _healthBar.SetHealthBar(xScale);
    }
}
