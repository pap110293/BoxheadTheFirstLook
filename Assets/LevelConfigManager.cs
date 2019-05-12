using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfigManager : MonoBehaviour
{
    public LevelCollection levelCollection;

    [HideInInspector]
    public LevelData currentLevelData;
    private int currentLevelIndex = -1;
    public int CurrentLevel { get { return currentLevelIndex + 1; } }

    void Awake()
    {
        MasterManager.levelConfigManager = this;
    }

    public LevelData GetNextLevelData()
    {
        if (IsLastlevel) return null;
        currentLevelIndex++;
        currentLevelData = levelCollection.records[currentLevelIndex];
        return currentLevelData;
    }

    public bool IsLastlevel
    {
        get
        {
            return currentLevelIndex == levelCollection.records.Length - 1;
        }
    }

    public LevelData GetConfigWithLevel(int Level)
    {
        foreach (var levelData in levelCollection.records)
        {
            //Debug.Log("level id: " + levelData.levelId);
            if (Level == levelData.levelId)
            {
                return levelData;
            }
        }
        return null;
    }

    public void Restart()
    {
        currentLevelIndex = -1;
    }
}
