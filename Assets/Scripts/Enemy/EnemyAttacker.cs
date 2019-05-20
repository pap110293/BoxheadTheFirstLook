using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    public float skillBlowFlySpeed = 8;
    
    public bool isValidAttack;
    public float attackCooldown = 1;
    public float actionAttackSpeed = 1;

    public Transform shootPoint;
    public Transform target;
    public GameObject skillBlowPrefabs;
    private float countDownAttacker;
    private SkillBlow.SkillType skillType;
    private bool isInitlized = false;
    public void InitAttacker(float _attackSpeed , float _skillFlySpeed, SkillBlow.SkillType _skillType)
    {
        attackCooldown = _attackSpeed;
        skillBlowFlySpeed = _skillFlySpeed;
        skillType = _skillType;
        isInitlized = true;
    }
    private void Start()
    {
        if (!shootPoint) shootPoint = this.transform;
        countDownAttacker = 0;
    }
    public void EnemyUpdateTargetAttack(Transform _target)
    {
        if (_target) target = _target;
    }
    public void ActionAttack()
    {
        EnemyAttack(target, skillBlowFlySpeed);
    }
    void EnemyAttack(Transform target, float flySpeed)
    {
        if (!shootPoint || !target) return;
        Transform _targetAim = target.transform;
        var SkillBlow = Instantiate(skillBlowPrefabs, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<SkillBlow>();
        SkillBlow.transform.position = shootPoint.position;
        SkillBlow.InitSkillBlow(shootPoint, target, _targetAim, flySpeed, skillType);
        countDownAttacker = attackCooldown;
    }
    private void Update()
    {
        countDownAttacker = countDownAttacker -Time.deltaTime;
        if (countDownAttacker <= 0)
        {
            isValidAttack = true;
        }
        else
        {
            isValidAttack = false;
        } 
         
    }
}
