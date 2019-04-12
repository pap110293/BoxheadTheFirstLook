using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private enum AnimState
    {
        Idle,
        RunForward,
        MeleeAttack,
        Dead
    }
    public Animator animator;
    public Transform target;
    public float movementSpeed;
    public Movement planeMovement;

    public float AttackCountDown;
    public float TimeCount;
    public GameObject ArrowPrefabs;
    public float ArrowFlySpeed = 8;
    public Transform ShootPoint;
    public TeamManager teamManager;
    void Start()
    {
        planeMovement = this.GetComponent<Movement>();

        AttackCountDown = 0;
        TimeCount = 1;
        //if (!animator) animator.speed = TimeCount * 120 / 100;
        teamManager = this.gameObject.GetComponent<TeamManager>();
    }
    public void UpdateChasing()
    {
        //transform.LookAt(target);
        //transform.position = Vector3.MoveTowards(transform.position, target.position, movementSpeed* Time.deltaTime);
        //if (!animator) animator.SetInteger("AnimState", (int)AnimState.RunForward);
        if (target) planeMovement.SetMove(target);

    }
    public void StopChasing()
    {
        //if (this.gameObject.GetComponent<Health>().currHealth <= 0)
        //{
        //    animator.SetInteger("AnimState", (int)AnimState.Dead);
        //    return;
        //}
        //if(!animator) animator.SetInteger("AnimState", (int)AnimState.Idle);
        planeMovement.Stop();
    }
    public void UpdateAttacking()
    {
        //if (this.gameObject.GetComponent<Health>().currHealth <= 0)
        //{
        //    animator.SetInteger("AnimState", (int)AnimState.Dead);
        //    return;
        //}
        transform.LookAt(target);
        //if (!animator) animator.SetInteger("AnimState", (int)AnimState.MeleeAttack);
        SwordUpdateAttack();
    }
    #region Attack    
    private void Update()
    {
        if (teamManager != null) target = teamManager.target;
    }
    void SwordUpdateAttack()
    {
        AttackCountDown -= Time.deltaTime;
        if (AttackCountDown <= 0)
        {
            Invoke("ActionSwordAttack", TimeCount / 2);
            AttackCountDown = TimeCount;
        }
    }
    public void ActionSwordAttack()
    {
        EnemyAttack(target, ArrowFlySpeed);
    }
    void EnemyAttack(Transform target, float flySpeed)
    {
        if (!ShootPoint || !target) return;
        var _spineStruct = target.gameObject.GetComponent<SpineStruct>();
        if (!_spineStruct) return;
        Transform _targetAim = _spineStruct.GetBody();
        var SwordBlow = Instantiate(ArrowPrefabs, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<SkillBlow>();
        SwordBlow.transform.position = ShootPoint.position;
        SwordBlow.InitArrow(ShootPoint, target, _targetAim, flySpeed);
    }
    #endregion
}
