//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------

using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(NetworkIdentity))]

public class EnemySpawner : NetworkBehaviour
{
    public bool AutoSpawn;
    public GameObject[] Objectives;
    public GameObject[] ObjectSpawn;
    public GameObject[] SubSpawner;
    public float TimeSpawn = 3;
    public int MaxObject = 10;
    public string PlayerTag = "Player";
    public bool TeamConfig = false;
    public byte Team = 0;
    public bool PlayerEnter = true;
    private float timetemp = 0;
    private int indexSpawn;
    private List<GameObject> spawnList = new List<GameObject>();
    [HideInInspector]
    public bool OnActive;

    void Start()
    {
        this.GetComponent<NetworkIdentity>().serverOnly = true;
        timetemp = Time.time;
    }


    void Update()
    {
        OnActive = false;
        if (!isServer)
            return;

        if (PlayerEnter)
        {
            // check if player is enter this area
            GameObject[] playersaround = GameObject.FindGameObjectsWithTag(PlayerTag);
            for (int p = 0; p < playersaround.Length; p++)
            {
                if (Vector3.Distance(this.transform.position, playersaround[p].transform.position) < this.transform.localScale.x)
                {
                    OnActive = true;
                }
            }
        }
        else
        {
            OnActive = true;
        }

        if (!OnActive)
            return;

        if (AutoSpawn)
        {
            if (Time.time > timetemp + TimeSpawn)
            {
                Spawn();
                timetemp = Time.time;
            }
        }
    }

    public GameObject[] Spawn(int number)
    {
        GameObject[] spawneds = new GameObject[number];
        for (int i = 0; i < spawneds.Length; i++)
        {
            spawneds[i] = Spawn();
        }
        return spawneds;
    }

    public GameObject Spawn()
    {
        GameObject obj = null;
        int objectcount = ObjectSpawnedCount();
        indexSpawn = Random.Range(0, ObjectSpawn.Length);

        if (ObjectSpawn[indexSpawn] == null)
            return null;

        if (objectcount < MaxObject)
        {
            if (SubSpawner.Length > 0)
            {
                int s = Random.Range(0, SubSpawner.Length);
                Vector3 spawnPoint = DetectGround(SubSpawner[s].transform.position + new Vector3(Random.Range(-(int)(SubSpawner[s].transform.localScale.x / 2.0f), (int)(SubSpawner[s].transform.localScale.x / 2.0f)), 0, Random.Range((int)(-SubSpawner[s].transform.localScale.z / 2.0f), (int)(SubSpawner[s].transform.localScale.z / 2.0f))));
                obj = UnitZ.gameNetwork.RequestSpawnObject(ObjectSpawn[indexSpawn].gameObject, spawnPoint, Quaternion.identity);
            }
            else
            {

                Vector3 spawnPoint = DetectGround(transform.position + new Vector3(Random.Range(-(int)(this.transform.localScale.x / 2.0f), (int)(this.transform.localScale.x / 2.0f)), 0, Random.Range((int)(-this.transform.localScale.z / 2.0f), (int)(this.transform.localScale.z / 2.0f))));
                obj = UnitZ.gameNetwork.RequestSpawnObject(ObjectSpawn[indexSpawn].gameObject, spawnPoint, Quaternion.identity);

            }

            if (obj)
            {
                DamageManager damagemanager = obj.GetComponent<DamageManager>();
                if (damagemanager != null && TeamConfig)
                {
                    damagemanager.Team = Team;
                }
                AICharacterShooterNav ai = obj.GetComponent<AICharacterShooterNav>();
                if (ai != null)
                {
                    if (Objectives != null && Objectives.Length > 0)
                        ai.ObjectiveTarget = Objectives[Random.Range(0, Objectives.Length)];
                }
                spawnList.Add(obj);

            }
        }
        return obj;
    }


    int ObjectSpawnedCount()
    {
        // checking a number of all objects. that's was spawn with this spawner
        int num = 0;
        foreach (var obj in spawnList)
        {
            if (obj != null)
                num++;
        }
        return num;
    }

    // draw gizmose on Editor
    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2);
        Gizmos.DrawWireCube(transform.position, this.transform.localScale);
        Handles.Label(transform.position, "Enemy Spawner");
#endif
    }

    Vector3 DetectGround(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, -Vector3.up, out hit, 1000.0f))
        {
            return hit.point;
        }
        return position;
    }

}
