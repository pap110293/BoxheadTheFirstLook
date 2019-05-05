using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    public LevelCollection levelCollection;
    void Start()
    {
        MasterManager.levelConfig = this;
        //foreach (var levelData in levelCollection.records)
        //{
        //    Debug.Log("level id: " + levelData.levelId);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public LevelData GetConfigWithLevel(int Level)
    {
        foreach (var levelData in levelCollection.records)
        {
            //Debug.Log("level id: " + levelData.levelId);
            if(Level == levelData.levelId)
            {
                return levelData;
            }
        }
        return null;
    }
}
