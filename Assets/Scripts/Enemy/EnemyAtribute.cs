using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtribute : MonoBehaviour
{
    public int HP;
    public int Amor;
    public float movementSpeed;
    public float flyHeight;
    public Movement.TypeMove typeMove;
    public float attackCooldown;
    public float skillFlySpeed;
    public SkillBlow.SkillType skillType;

    public void SetAtribute( float _movementSpeed,float _flyHeight,float _attackCooldown, float _skillFlySpeed)
    {
        movementSpeed = _movementSpeed;
        flyHeight = _flyHeight;
        attackCooldown = _attackCooldown;
        skillFlySpeed = _skillFlySpeed;
    }
}
