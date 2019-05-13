using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyLevelData : MonoBehaviour
{
    public int Level;
    public float Health;
    public float Damage;
    public float MoveSpeed;
    public float AttackSpeed;
}
[CreateAssetMenu(fileName = "EnemyAttributeData", menuName = "Assets/Create Enemy Attribute Collection")]
public class EnemyAttributeCollection : ScriptableObject
{
    public EnemyLevelData[] records;
}
