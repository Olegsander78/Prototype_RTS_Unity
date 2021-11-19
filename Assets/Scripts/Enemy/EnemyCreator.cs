using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public Transform Spawn;
    public float CreationPeriod;
    public GameObject EnemyPrefab;

    public int MaxNumberSpawnEnemies;
    private Enemy[] _spawnEnemies;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        _spawnEnemies = FindObjectsOfType<Enemy>();
        if (_spawnEnemies.Length < MaxNumberSpawnEnemies)
        {
            if (_timer > CreationPeriod)
            {
                _timer = 0;
                Instantiate(EnemyPrefab, Spawn.position, Spawn.rotation);
            }
        }
    }   
}
