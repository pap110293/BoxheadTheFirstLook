using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawEnemyManager : MonoBehaviour
{
    public List<Spawner> spawners;
    public int totalEnemy;
    void Start()
    {
        MasterManager.spawEnemyManager = this;
        //InitSpawners(MasterManager.levelConfig.GetConfigWithLevel(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitSpawners(LevelData _levelData)
    {        
        foreach(var spawner in spawners)
        {
            spawner.StopSpawn();
            spawner.spawnerObjects = _levelData.monsters;
            spawner.numberOfObject = _levelData.numberOfObject;
            spawner.duration = _levelData.duration;
            spawner.StartSpawn();
        }
        
    }
}
