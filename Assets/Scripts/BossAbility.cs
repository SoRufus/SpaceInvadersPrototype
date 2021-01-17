using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbility : MonoBehaviour
{
    [HideInInspector] [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int spawnEnemyTime;
    [SerializeField] private float spawnPointRange;


    void Start()
    {
        InvokeRepeating("SpawnEnemy",spawnEnemyTime, spawnEnemyTime);
    }

    void SpawnEnemy()
    {
        var spawnedObject = Instantiate(enemyPrefab, new Vector2(transform.position.x + Random.Range(-spawnPointRange, spawnPointRange), transform.position.y + Random.Range(-spawnPointRange, spawnPointRange)), transform.rotation);
        spawnedObject.transform.parent = gameObject.transform;
    }
}
