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
        [Range(0, 100.0f)]
        public float percent;
    }

    public SpawnerObject[] spawnerObjects;
    public float spawnSpeed = 1.0f;
    public Vector3 size = new Vector3(10.0f, 1.0f, 10.0f);
    public bool isSpawning { get { return spawning; } }

    private float timing = 0.0f;
    private bool spawning = false;

    private void Start()
    {
        StartSpawn();
    }

    private void Update()
    {
        if (!spawning)
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
        spawning = true;
    }

    public void StopSpawn()
    {
        spawning = false;
    }

    private void Spawn()
    {
        var randomNum = UnityEngine.Random.value;

        foreach (var ob in spawnerObjects)
        {
            if (randomNum < ob.percent/100.0f)
            {
                var x = UnityEngine.Random.Range(transform.position.x - size.x/2, transform.position.x + size.x/2);
                var z = UnityEngine.Random.Range(transform.position.z - size.z/2, transform.position.z + size.z/2);
                var y = transform.position.y;
                var position = new Vector3(x, y, z);
                Instantiate(ob.Obj, position, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
