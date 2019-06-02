using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    [Serializable]
    public struct SpawnerObject
    {
        public GameObject Obj;
        public int level;
        public EnemyManager.EnemyType enemyType;
        [Range(0, 100.0f)]
        public float percent;
    }

    public SpawnerObject[] spawnerObjects;
    public float duration = 10.0f;
    public int numberOfObject = 10;
    public Vector3 size = new Vector3(10.0f, 1.0f, 10.0f);
    public bool isSpawning { get { return spawning; } }

    private int counting = 0;
    private float spawnSpeed;
    private float timing = 0.0f;
    [SerializeField]
    private bool spawning = false;

    private void Start()
    {
        counting = 0;
        spawnSpeed = duration / numberOfObject;
    }

    private void Update()
    {
        if (!spawning || counting >= numberOfObject)
            return;

        if (timing <= 0)
        {
            Spawn();
            timing = spawnSpeed;
        }
        else
        {
            timing -= Time.deltaTime;
        }
    }

    public void StartSpawn()
    {
        counting = 0;
        timing = 0f;
        spawning = true;
        spawnSpeed = duration / numberOfObject;
    }

    public void StopSpawn()
    {
        spawning = false;
    }

    private void Spawn()
    {
        float totalRate = TotalSpawnRate();
        var randomNum = UnityEngine.Random.Range(0f, totalRate);

        foreach (var ob in spawnerObjects)
        {
            if (randomNum < ob.percent)
            {
                counting++;
                SpawnZombie(ob);
                if (counting >= numberOfObject)
                {
                    StopSpawn();
                }
                return;
            }
            randomNum -= ob.percent;
        }
    }

    private void SpawnZombie(SpawnerObject ob)
    {
        var x = UnityEngine.Random.Range(transform.position.x - size.x / 2, transform.position.x + size.x / 2);
        var z = UnityEngine.Random.Range(transform.position.z - size.z / 2, transform.position.z + size.z / 2);
        var y = transform.position.y;
        var position = new Vector3(x, y, z);
        //var Enemy = Instantiate(ob.Obj, position, Quaternion.identity);
        //vinh add
        //Enemy.GetComponent<EnemyManager>().enemyAtribute = MasterManager.enemyLevelConfigManager.GetEnemyAtribute(ob.level, ob.enemyType).atribute;
        var EnemyUnit = MasterManager.enemyLevelConfigManager.GetEnemyAtribute(ob.level, ob.enemyType);
        if (EnemyUnit != null)
        {
            var Enemy = Instantiate(EnemyUnit.enemyPrefabs, position, Quaternion.identity);
            Enemy.GetComponent<EnemyManager>().enemyAtribute = EnemyUnit.atribute;
            MasterManager.spawEnemyManager.totalEnemy++;
        }

        //MasterManager.spawEnemyManager.totalEnemy++;
    }

    private float TotalSpawnRate()
    {
        var total = 0f;
        foreach (var ob in spawnerObjects)
        {
            total += ob.percent;
        }
        return total;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
