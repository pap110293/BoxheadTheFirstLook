using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int levelId;
    public float speed;
    public float duration;
    public Spawner.SpawnerObject[] monsters;
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Assets/Create Level Collection")]
public class LevelCollection: ScriptableObject
{
    public LevelData[] records;
}
