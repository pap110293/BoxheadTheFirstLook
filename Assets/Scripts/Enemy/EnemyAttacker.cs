﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    public float AttackCountDown;
    public float TimeCount;
    public float SkillBlowFlySpeed = 8;
    public Transform ShootPoint;
    public Transform target;
    public GameObject skillBlowPrefabs;
    private void Start()
    {
        AttackCountDown = 0;
        TimeCount = 1;
        //if (!animator) animator.speed = TimeCount * 120 / 100;
    }
    public void EnemyUpdateAttack(Transform _target)
    {
        if (_target) target = _target;
         AttackCountDown -= Time.deltaTime;
        if (AttackCountDown <= 0)
        {
            Invoke("ActionSwordAttack", TimeCount / 2);
            AttackCountDown = TimeCount;
        }
    }
    public void ActionSwordAttack()
    {
        EnemyAttack(target, SkillBlowFlySpeed);
    }
    void EnemyAttack(Transform target, float flySpeed)
    {
        if (!ShootPoint || !target) return;
        //var _spineStruct = target.gameObject.GetComponent<SpineStruct>();
        //if (!_spineStruct) return;
        //Transform _targetAim = _spineStruct.GetBody();
        Transform _targetAim = target.transform;
        var SkillBlow = Instantiate(skillBlowPrefabs, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<SkillBlow>();
        SkillBlow.transform.position = ShootPoint.position;
        SkillBlow.InitSkillBlow(ShootPoint, target, _targetAim, flySpeed,false);
    }
}