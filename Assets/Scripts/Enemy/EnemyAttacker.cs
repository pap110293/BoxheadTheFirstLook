using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    //public float AttackCountDown;
    //public float TimeCount;
    public float AttackSpeed = 1;
    public float SkillBlowFlySpeed = 8;
    public Transform ShootPoint;
    public Transform target;
    public GameObject skillBlowPrefabs;
    private void Start()
    {
        //AttackCountDown = 0;
        //TimeCount = 1;
        if (!ShootPoint) ShootPoint = this.transform;
    }
    public void EnemyUpdateTargetAttack(Transform _target)
    {
        if (_target) target = _target;
    }
    public void ActionAttack()
    {
        EnemyAttack(target, SkillBlowFlySpeed);
    }
    void EnemyAttack(Transform target, float flySpeed)
    {
        if (!ShootPoint || !target) return;
        Transform _targetAim = target.transform;
        var SkillBlow = Instantiate(skillBlowPrefabs, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<SkillBlow>();
        SkillBlow.transform.position = ShootPoint.position;
        //SkillBlow.InitSkillBlow(ShootPoint, target, _targetAim, flySpeed);
        SkillBlow.InitSkillBlow(ShootPoint, target, target.transform.position, flySpeed);
    }
}
