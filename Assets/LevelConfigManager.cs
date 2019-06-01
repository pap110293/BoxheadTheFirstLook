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

    public LevelData GetConfigWithLevel(int LevelIndex)
    {
        for (int i = 0; i < levelCollection.records.Length; i++)
        {
            if (LevelIndex == i)
            {
                return levelCollection.records[i];
            }
        }

        return null;
    }

    public void Restart()
    {
        currentLevelIndex = -1;
    }
}
