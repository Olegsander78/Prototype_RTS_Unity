using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public enum UnitState
{
    Idle,
    WalkToPoint,
    WalkToEnemy,
    Attack
}

public class Knight : Unit
{
    public UnitState CurrentUnitState;

    public Vector3 TargetPoint;
    public Enemy TargetEnemy;
    public LairOfEnemies TargetEnemyLair;
    public float DistanceToFollowEnemy = 5f;
    public float DistanceToFollowEnemyLair = 8f;
    public float DistanceToAttack = 1f;

    public float UnitAttackPeriod = 1f;
    private float _timer;
    public int UnitDamageValue = 1;

    public Animator Animator;

    public override void Start()
    {
        base.Start();
        SetStateUnit(UnitState.Idle);
    }

    void Update()
    {
        if (CurrentUnitState == UnitState.Idle)
        {
            Animator.SetTrigger("Idle");
            FindClosestEnemy();
            FindClosestEnemyLair();
        }
        else if (CurrentUnitState == UnitState.WalkToPoint)
        {
            Animator.SetTrigger("Walk");
            FindClosestEnemy();
            FindClosestEnemyLair();
        }
        else if (CurrentUnitState == UnitState.WalkToEnemy)
        {
            Animator.SetTrigger("Walk");
            if (TargetEnemy)
            {
                NavMeshAgent.SetDestination(TargetEnemy.transform.position);
                float distance = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                if (distance > DistanceToFollowEnemy)
                {
                    Animator.SetTrigger("Walk");
                    SetStateUnit(UnitState.WalkToPoint);
                }
                if (distance < DistanceToAttack)
                {
                    Animator.SetTrigger("Attack");
                    SetStateUnit(UnitState.Attack);
                }
            }else if (TargetEnemyLair)
            {
                NavMeshAgent.SetDestination(TargetEnemyLair.transform.position);
                float distance = Vector3.Distance(transform.position, TargetEnemyLair.transform.position);
                if (distance > DistanceToFollowEnemyLair)
                {
                    Animator.SetTrigger("Walk");
                    SetStateUnit(UnitState.WalkToPoint);
                }
                if (distance < DistanceToAttack)
                {
                    Animator.SetTrigger("Attack");
                    SetStateUnit(UnitState.Attack);
                }
            }
            else
            {
                Animator.SetTrigger("Walk");
                SetStateUnit(UnitState.WalkToPoint);
            }

        }
        else if (CurrentUnitState == UnitState.Attack)
        {
            Animator.SetTrigger("Attack");
            if (TargetEnemy)
            {
                NavMeshAgent.SetDestination(TargetEnemy.transform.position);
                float distance = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                if (distance > DistanceToAttack)
                {
                    Animator.SetTrigger("Walk");
                    SetStateUnit(UnitState.WalkToEnemy);
                }
                _timer += Time.deltaTime;
                if (_timer > UnitAttackPeriod)
                {
                    _timer = 0f;
                    Animator.SetTrigger("Attack");
                    TargetEnemy.TakeDamage(UnitDamageValue);
                }
            }
            else if (TargetEnemyLair)
            {
                NavMeshAgent.SetDestination(TargetEnemyLair.transform.position);
                float distance = Vector3.Distance(transform.position, TargetEnemyLair.transform.position);
                if (distance > DistanceToAttack)
                {
                    Animator.SetTrigger("Walk");
                    SetStateUnit(UnitState.WalkToEnemy);
                }
                _timer += Time.deltaTime;
                if (_timer > UnitAttackPeriod)
                {
                    _timer = 0f;
                    Animator.SetTrigger("Attack");
                    TargetEnemyLair.GetDamage(UnitDamageValue);
                }
            }

            else 
            {
                Animator.SetTrigger("Walk");
                SetStateUnit(UnitState.WalkToPoint);
            }
        }
    }

    public void SetStateUnit(UnitState unitState)
    {
        CurrentUnitState = unitState;
        if (CurrentUnitState == UnitState.Idle)
        {
            Animator.SetTrigger("Idle");           
        }
        else if (CurrentUnitState == UnitState.WalkToPoint)
        {
            Animator.SetTrigger("Walk");
        }
        else if (CurrentUnitState == UnitState.WalkToEnemy)
        {
            Animator.SetTrigger("Walk");
        }
        else if (CurrentUnitState == UnitState.Attack)
        {
            _timer = 0;
            Animator.SetTrigger("Attack");
        }
    }

    
    public void FindClosestEnemy()
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        float minDistance = Mathf.Infinity;
        Enemy closestEnemy = null;

        for (int i = 0; i < allEnemies.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allEnemies[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = allEnemies[i];
            }
        }
        if (minDistance < DistanceToFollowEnemy)
        {
            TargetEnemy = closestEnemy;
            Animator.SetTrigger("Walk");
            SetStateUnit(UnitState.WalkToEnemy);
        }
    }

    public void FindClosestEnemyLair()
    {
        LairOfEnemies[] allEnemiesLair = FindObjectsOfType<LairOfEnemies>();
        float minDistance = Mathf.Infinity;
        LairOfEnemies closestEnemyLair = null;

        for (int i = 0; i < allEnemiesLair.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allEnemiesLair[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemyLair = allEnemiesLair[i];
            }
        }
        if (minDistance < DistanceToFollowEnemyLair)
        {
            TargetEnemyLair = closestEnemyLair;
            Animator.SetTrigger("Walk");
            SetStateUnit(UnitState.WalkToEnemy);
        }
    }

    public override void Die()
    {
        Animator.SetTrigger("Death");
        base.Die();

    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToAttack);
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToFollowEnemy);
    }
#endif 
}
