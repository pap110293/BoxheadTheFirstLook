using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerController : MonoBehaviour
{
    public Spawner.SpawnerObject[] spawnerObjects;

    void Start()
    {
        MasterManager.itemSpawnerController = this;
    }

    public GameObject GetItem()
    {
        float totalRate = TotalSpawnRate();
        var randomNum = Random.Range(0f, totalRate);

        foreach (var ob in spawnerObjects)
        {
            if (randomNum < ob.percent)
            {
                return ob.Obj;
            }
            randomNum -= ob.percent;
        }
        return null;
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
}
