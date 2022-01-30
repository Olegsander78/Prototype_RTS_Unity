using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    WalkToBuilding,
    WalkToUnit,
    Attack
}

public class Enemy : MonoBehaviour
{
    public EnemyState CurrentEnemyState;

    public int EnemyHealth;
    private int _maxEnemyHealth;
    public Building TargetBuilding;
    public Unit TargetUnit;
    public float DistanceToFollow = 7f;
    public float DistanceToAttack = 1f;

    public NavMeshAgent NavMeshAgent;

    public float EnemyAttackPeriod = 1f;
    private float _timer;
    public int EnemyDamageValue = 1;

    public GameObject HealthBarPrefab;
    public float HeigthToHealthBar;
    private HealthBar _healthBar;

    public Animator Animator;
    private void Start()
    {
        SetStateEnemy(EnemyState.WalkToBuilding);

        _maxEnemyHealth = EnemyHealth;
        GameObject healthBar = Instantiate(HealthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.SetHealthBarHeightAtGround(HeigthToHealthBar);
        _healthBar.Setup(transform);
    }

    void Update()
    {
        if (CurrentEnemyState == EnemyState.Idle)
        {
            FindClosestBuilding();
            if (TargetBuilding)
            {
                SetStateEnemy(EnemyState.WalkToBuilding);
                Animator.SetTrigger("Walk");
            }
            FindClosestUnit();            
        }
        else if (CurrentEnemyState == EnemyState.WalkToBuilding)
        {
            FindClosestUnit();

            if (TargetBuilding)
            {
                NavMeshAgent.SetDestination(TargetBuilding.transform.position);
                float distance = Vector3.Distance(transform.position, TargetBuilding.transform.position);
                if (distance > DistanceToFollow)
                {
                    SetStateEnemy(EnemyState.WalkToBuilding);
                    Animator.SetTrigger("Walk");
                }
                if (distance < DistanceToAttack)
                {
                    SetStateEnemy(EnemyState.Attack);
                    Animator.SetTrigger("Attack");
                }
            }
            else
            {
                SetStateEnemy(EnemyState.WalkToBuilding);
                Animator.SetTrigger("Walk");
            }

            if (TargetBuilding == null)
            {
                SetStateEnemy(EnemyState.Idle);
                Animator.SetTrigger("Idle");
            }
        }
        else if (CurrentEnemyState == EnemyState.WalkToUnit)
        {
            if (TargetUnit)
            {
                NavMeshAgent.SetDestination(TargetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);
                if (distance > DistanceToFollow)
                {
                    SetStateEnemy(EnemyState.WalkToBuilding);
                    Animator.SetTrigger("Walk");
                }
                if (distance < DistanceToAttack)
                {
                    SetStateEnemy(EnemyState.Attack);
                    Animator.SetTrigger("Attack");
                }
            }
            else
            {
                SetStateEnemy(EnemyState.WalkToBuilding);
                Animator.SetTrigger("Walk");
            }
            
        }else if (CurrentEnemyState == EnemyState.Attack)
        {
            if (TargetUnit)
            {
                NavMeshAgent.SetDestination(TargetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);
                if (distance > DistanceToAttack)
                {
                    SetStateEnemy(EnemyState.WalkToUnit);
                    Animator.SetTrigger("Walk");
                }
                _timer += Time.deltaTime;
                if (_timer > EnemyAttackPeriod)
                {
                    _timer = 0f;
                    Animator.SetTrigger("Attack");
                    TargetUnit.TakeDamage(EnemyDamageValue);
                }
            }else
            {
                SetStateEnemy(EnemyState.WalkToBuilding);
                Animator.SetTrigger("Walk");
            }
            if (TargetBuilding)
            {
                NavMeshAgent.SetDestination(TargetBuilding.transform.position);
                float distance = Vector3.Distance(transform.position, TargetBuilding.transform.position);
                if (distance > DistanceToAttack)
                {
                    SetStateEnemy(EnemyState.WalkToBuilding);
                    Animator.SetTrigger("Walk");
                }
                _timer += Time.deltaTime;
                if (_timer > EnemyAttackPeriod)
                {
                    _timer = 0f;
                    Animator.SetTrigger("Attack");
                    TargetBuilding.GetDamage(EnemyDamageValue);
                }
            }
            else
            {
                SetStateEnemy(EnemyState.WalkToBuilding);
                Animator.SetTrigger("Walk");
            }
        }
    }

    public void SetStateEnemy(EnemyState enemyState)
    {
        CurrentEnemyState = enemyState;
        if (CurrentEnemyState == EnemyState.Idle)
        {
            Animator.SetTrigger("Idle");
        }
        else if (CurrentEnemyState == EnemyState.WalkToBuilding)
        {
            FindClosestBuilding();
            if (TargetBuilding)
            {
                NavMeshAgent.SetDestination(TargetBuilding.transform.position);
                Animator.SetTrigger("Walk");
            }
            else
            {
                SetStateEnemy(EnemyState.Idle);
                Animator.SetTrigger("Idle");
            }
        }
        else if (CurrentEnemyState == EnemyState.WalkToUnit)
        {
            Animator.SetTrigger("Walk");
        }
        else if (CurrentEnemyState == EnemyState.Attack)
        {
            _timer = 0;
            Animator.SetTrigger("Attack");
        }
    }

    public void FindClosestBuilding()
    {
        Building[] allBuildings = FindObjectsOfType<Building>();
        float minDistance = Mathf.Infinity;
        Building closestBuilding = null;

        for (int i = 0; i < allBuildings.Length; i++)
        {
            if (allBuildings[i].IsPlaced)
            {
                float distance = Vector3.Distance(transform.position, allBuildings[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestBuilding = allBuildings[i];
                }
            }
        }
        TargetBuilding = closestBuilding;
    }
    public void FindClosestUnit()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();
        float minDistance = Mathf.Infinity;
        Unit closestUnit = null;

        for (int i = 0; i < allUnits.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allUnits[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestUnit = allUnits[i];
            }
        }
        if (minDistance < DistanceToFollow)
        {
            TargetUnit = closestUnit;
            SetStateEnemy(EnemyState.WalkToUnit);
        }

    }

    public void TakeDamage(int damageValue)
    {
        EnemyHealth -= damageValue;
        SetHealth(EnemyHealth, _maxEnemyHealth);
        if (EnemyHealth <= 0)
        {
            DieEnemy(); 
        }
    }
    
    public void DieEnemy()
    {
        FindObjectOfType<Statistics>().AddEnemyDeath();
        Animator.SetTrigger("Death");
        _healthBar.DelHealthBar();
        if (_healthBar)
        {
            Destroy(_healthBar.gameObject);
        }
        Destroy(gameObject.gameObject, 1.5f);
    }

    public void SetHealth(int health, int maxHealth)
    {
        if (health > 0)
        {
            float xScale = (float)health / maxHealth;
            xScale = Mathf.Clamp01(xScale);
            _healthBar.SetHealthBar(xScale);
        }
        else
        {
            float xScale = 0f;
            _healthBar.SetHealthBar(xScale);
        }
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToAttack);
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToFollow);
    }
#endif
}
