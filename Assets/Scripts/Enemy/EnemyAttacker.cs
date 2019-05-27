using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    public float skillBlowFlySpeed = 8;
    
    public bool isValidAttack;
    public float attackCooldown = 1;
    public float actionAttackSpeed = 1;
    public int damage = 1;
    public float CurrentMana = 1;
    public float timeCastingSkill = 0;
    public bool UnlockCastSkill = false;

    public Transform shootPoint;
    public Transform target;
    public GameObject skillBlowPrefabs;
    private float countDownAttacker;
    private SkillBlow.SkillType skillType;
    private bool isInitlized = false;
    public void InitAttacker(float _attackSpeed, float _skillFlySpeed, int _damage, SkillBlow.SkillType _skillType)
    {
        attackCooldown = _attackSpeed;
        skillBlowFlySpeed = _skillFlySpeed;
        skillType = _skillType;
        damage = _damage;
        isInitlized = true;
    }
    private void Start()
    {
        if (!shootPoint) shootPoint = this.transform;
        countDownAttacker = 0;
    }
    private void Update()
    {
        countDownAttacker = countDownAttacker - Time.deltaTime;
        CurrentMana += Time.deltaTime;
        if (countDownAttacker <= 0)
        {
            isValidAttack = true;
        }
        else
        {
            isValidAttack = false;
        }
    }
    void EnemyDefaultAttack(Transform target, float flySpeed)
    {
        if (!shootPoint || !target) return;
        Transform _targetAim = target.transform;
        var SkillBlow = Instantiate(skillBlowPrefabs, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<SkillBlow>();
        SkillBlow.transform.position = shootPoint.position;
        SkillBlow.InitSkillBlow(shootPoint, target, _targetAim, flySpeed, damage, skillType);
        countDownAttacker = attackCooldown;
    }
    public void InitCastSkill(EnemyAnimState _animState)
    {
        timeCastingSkill = 3;
        countDownAttacker = timeCastingSkill + 1;
        CurrentMana = 0;
        _animState.SetAnim(EnemyAnimState.AnimState.CastingSkill, actionAttackSpeed);
    }
    public void UpdateAtacker(Transform _target, EnemyAnimState _animState)
    {
        if (_target) target = _target;
        if (isValidAttack)
        {
            switch (CheckValidSkill())
            {
                case 1:
                    {                        
                        InitCastSkill(_animState);
                        break;
                    }
                default:
                    {
                        if (timeCastingSkill <= 0) _animState.SetAnim(EnemyAnimState.AnimState.DefaultAttack, actionAttackSpeed);
                        break;
                    }
            }
        }
        else
        {
            if (timeCastingSkill <= 0) _animState.SetAnim(EnemyAnimState.AnimState.Idle, 1);
        }
    }
    public int CheckValidSkill()
    {
        if (UnlockCastSkill && CurrentMana >= 10)
        {
            return 1;
        }
        return 0;
    }
    public void DefaultAttackEvent()
    {
        EnemyDefaultAttack(target, skillBlowFlySpeed);
    }
    public void CastSkillBeginEvent()
    {
        this.StartCoroutine(SkillCasting());
    }
    IEnumerator SkillCasting()
    {
        float _timeWait = 0.3f;
        while(timeCastingSkill > 0)
        {
            EnemyDefaultAttack(target, skillBlowFlySpeed);
            yield return new WaitForSecondsRealtime(_timeWait);
            timeCastingSkill -= _timeWait;
        }
        yield break;
    }


}
