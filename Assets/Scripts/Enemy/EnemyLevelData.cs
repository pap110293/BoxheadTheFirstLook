using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyLevelDataUnit
{
    //public int level;
    public EnemyManager.EnemyType enemyType;
    public EnemyAtribute atribute;
    public GameObject enemyPrefabs;
    //public EnemyAtribute enemyAtribute;
    //public GameObject enemyprefabs;
    //public int HP;
    //public int amor;
    //public float movementSpeed;
    //public float flyHeight;
    //public int damage;
    //public Movement.TypeMove typeMove;
    //public float attackCooldown;
    //public float skillFlySpeed;
    //public SkillBlow.SkillType skillType;

}
[System.Serializable]
public class EnemyLevelData
{
    public int Level;
    public EnemyLevelDataUnit[] records;
}
[CreateAssetMenu(fileName = "EnemyLevelData", menuName = "Assets/Create Enemy Level Collection")]
public class EnemyLevelCollection : ScriptableObject
{
    public EnemyLevelData[] records;
}
