using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawEnemyManager : MonoBehaviour
{
    public List<Spawner> spawners;
    public int totalEnemy = 0;

    private void Awake()
    {

        MasterManager.spawEnemyManager = this;
    }

    public void InitSpawners(LevelData _levelData)
    {
        foreach (var spawner in spawners)
        {
            if (_levelData.monsters != null)
                spawner.spawnerObjects = _levelData.monsters.ToArray();
            spawner.numberOfObject = _levelData.totalSpawnObject / spawners.Count;
            spawner.duration = _levelData.duration;
            spawner.StopSpawn();
        }
    }

    public void StopAll()
    {
        foreach (var spawner in spawners)
        {
            spawner.StopSpawn();
        }
    }

    public void StartAll()
    {
        foreach (var spawner in spawners)
        {
            spawner.StartSpawn();
        }
    }

    public bool IsAllSpawnerStoped
    {
        get
        {
            foreach (var spawner in spawners)
            {
                if (spawner.isSpawning)
                    return false;
            }
            return true;
        }
    }
}
