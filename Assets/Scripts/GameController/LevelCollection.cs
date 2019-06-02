using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public float duration;
    public int totalSpawnObject;
    public List<Spawner.SpawnerObject> monsters;
    public GameObject boss;

}

[CreateAssetMenu(fileName = "LevelData", menuName = "Assets/Create Level Collection")]
public class LevelCollection : ScriptableObject
{
    public List<LevelData> records;
}

public static class CreateGameLevel
{
    [MenuItem("Custom/Game Levels/Create New Game Level Data")]
    public static void CreateGameLevelData()
    {
        LevelCollection levelCollection = ScriptableObject.CreateInstance<LevelCollection>();
        levelCollection.records = new List<LevelData>();

        var defaultValue = 10f;
        var percentForDuration = 0.02f;
        var percentFOrMonster = 0.06f;
        var value1 = defaultValue;
        var value2 = defaultValue;
        int level;
        for (int i = 0; i < 100; i++)
        {
            level = i / 10 + 1; // add 1 level for 10 wave
            value1 += value1 * percentForDuration;
            Debug.Log("value 1 = " + value1);
            value2 += value2 * percentFOrMonster;
            Debug.Log("value 2 = " + (int)value1);
            LevelData temp = new LevelData
            {
                duration = value1,
                totalSpawnObject = (int)value2,
                monsters = new List<Spawner.SpawnerObject>()
            };

            for (int j = 0; j < 4; j++)
            {
                var monster = new Spawner.SpawnerObject();
                if (j == 0)
                    monster.enemyType = EnemyManager.EnemyType.Knight;
                if (j == 1)
                    monster.enemyType = EnemyManager.EnemyType.Mage;
                if (j == 2)
                    monster.enemyType = EnemyManager.EnemyType.Bat;
                if (j == 3)
                    monster.enemyType = EnemyManager.EnemyType.Dragon;

                monster.level = level;

                temp.monsters.Add(monster);
            }

            levelCollection.records.Add(temp);
        }

        AssetDatabase.CreateAsset(levelCollection, "Assets/Data/MainLevelData.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = levelCollection;
    }
}

