using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 5;
    private int enemiesLeftToSpawn;
    public int enemiesAlive {get; private set;}

    [SerializeField] private float enemyPerSecond = 1f;
    private float timeSinceLastSpawn;
    [SerializeField] private float timeBetweenWave = 1f;

    [SerializeField] private int currentWave = 0;

    public bool isSpawning{get; private set;} = false;

    [Header("Events")]
    public static UnityEvent onDestroyEnemy = new UnityEvent();

    private void Awake()
    {
        onDestroyEnemy.AddListener(DestroyedEnemy);
    }
    private void Start()
    {
        currentWave = 0;
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if(!isSpawning)
            return;

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }

        if(timeSinceLastSpawn >= 1f/enemyPerSecond && enemiesLeftToSpawn > 0)
        {
            Spawn();

            enemiesLeftToSpawn--;

            enemiesAlive++;

            timeSinceLastSpawn = 0;
        }

        timeSinceLastSpawn += Time.deltaTime;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWave);

        isSpawning = true;
        enemiesLeftToSpawn = baseEnemies;
        currentWave++;
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;

        StartCoroutine(StartWave());
    }

    private void Spawn()
    {
        Instantiate(enemyPrefabs[0], LevelManager.main.startPoint.position , Quaternion.identity);
    }

    private void DestroyedEnemy()
    {
        enemiesAlive--;
    }
}
