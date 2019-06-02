using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public int level;
    public int HP;
    public int Amor;
    public float movementSpeed;
    public float flyHeight;
    public int damage;
    public Movement.TypeMove typeMove;
    public float attackCooldown;
    public float skillFlySpeed;
    public SkillBlow.SkillType skillType;
    public EnemyData()
    {
        level = 1;
        HP = 1;
        Amor = 0;
        movementSpeed = 0;
        flyHeight = 0;
        damage = 0;
        attackCooldown = 0;
        skillFlySpeed = 0;        
    }
    public void SetAtribute(int _level, int _hp, int _amor,float _movementSpeed,float _flyHeight,int _damage,float _attackCooldown, float _skillFlySpeed)
    {
        level = _level;
        movementSpeed = _movementSpeed;
        flyHeight = _flyHeight;
        damage = _damage;
        attackCooldown = _attackCooldown;
        skillFlySpeed = _skillFlySpeed;
        HP = _hp;
        Amor = _amor;
    }
}
