using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int levelId;
    public float duration;
    public int totalSpawnObject;
    public Spawner.SpawnerObject[] monsters;
    public GameObject boss;
    public Spawner.SpawnerObject[] items;
    

}

[CreateAssetMenu(fileName = "LevelData", menuName = "Assets/Create Level Collection")]
public class LevelCollection: ScriptableObject
{
    public LevelData[] records;
}
