using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    public LevelCollection levelCollection;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var levelData in levelCollection.records)
        {
            Debug.Log("level id: " + levelData.levelId);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
