using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    #region Singleton
    public static EnemyManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion


    public Transform levelCenter;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;


    public float currentSpwanTime;
    public float minSpawnTime;
    public float spawnTimeFactor; // use this to decrease spawn time
    float lastSpawnTime;
    float lastSpawnFactorUpdateTime;
    public float LEVEL_TIME;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time - lastSpawnTime >= currentSpwanTime)
        {
            SpawnEnemy();
        }

        if(Time.time - lastSpawnFactorUpdateTime >= LEVEL_TIME)
        {
            currentSpwanTime *= spawnTimeFactor;
            if (currentSpwanTime < minSpawnTime) currentSpwanTime = minSpawnTime;
            lastSpawnFactorUpdateTime = Time.time;
        }
        
    }

    GameObject newEnemy;
    public void SpawnEnemy()
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject randEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        newEnemy = Instantiate(randEnemy, sp.position, Quaternion.identity, transform);
        StartCoroutine(StartEnemyMove());
        lastSpawnTime = Time.time;
    }

    public void OnEnemyDeath(Enemy e)
    {
        PlayerBounty.Instance.AddBounty(e.bountyAmount);
        GameStats.Addbounty(e.bountyAmount);
        GameStats.AddKill();
    }

    IEnumerator StartEnemyMove()
    {
        yield return new WaitForSeconds(0.75f);
        newEnemy.GetComponentInChildren<BasicEnemy>().SetMoveDest(levelCenter.position);
    }

}
