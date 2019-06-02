using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelConfigManager : MonoBehaviour
{
    public EnemyLevelCollection enemyLevelCollection;

    public bool isSpawn = false;
    public int level = 1;
    public EnemyManager.EnemyType enemyType;

    private void Awake()
    {
        MasterManager.enemyLevelConfigManager = this;
    }

    void Start()
    {
        MasterManager.ResumeGame();
    }

    void PawnEnemy(EnemyData _enemyAtribute, GameObject _enemyPrefabs)
    {
        var Enemy = Instantiate(_enemyPrefabs, new Vector3(0, 0, 0), Quaternion.identity);
        Enemy.GetComponent<EnemyManager>().enemyAtribute = _enemyAtribute;
    }

    public EnemyLevelDataUnit GetEnemyAtribute(int _level, EnemyManager.EnemyType _enemyType)
    {
        if (enemyLevelCollection != null && _level <= enemyLevelCollection.records.Length)
        {
            _level = _level - 1;
            foreach (var item in enemyLevelCollection.records[_level].records)
            {
                if (item.enemyType == _enemyType) return item;
            }
        }
        return null;
    }
}
