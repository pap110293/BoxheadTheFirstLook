using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    
    public Transform target;
    public float movementSpeed;
    public Movement planeMovement;
    public EnemyAttacker enemyAttacker;
    public TeamManager teamManager;
    public EnemyAnimState animState;
    void Start()
    {
        planeMovement = this.GetComponent<Movement>();
        //if (!animator) animator.speed = TimeCount * 120 / 100;
        teamManager = this.gameObject.GetComponent<TeamManager>();
        enemyAttacker = this.GetComponent<EnemyAttacker>();
        animState = this.GetComponent<EnemyAnimState>();
    }
    public void UpdateChasing()
    {
        if (target) planeMovement.SetMove(target);
        animState.SetAnim(EnemyAnimState.AnimState.RunForward);
    }
    public void StopChasing()
    {
        animState.SetAnim(EnemyAnimState.AnimState.Idle);
        planeMovement.Stop();
    }
    public void UpdateAttacking()
    {
        transform.LookAt(target);
        animState.SetAnim(EnemyAnimState.AnimState.DefaultAttack);
        enemyAttacker.EnemyUpdateAttack(target);
    }
    #region Attack    
    private void Update()
    {
        if (teamManager != null) target = teamManager.target;
    }    
    public void DefaultAttackEvent()
    {
        enemyAttacker.ActionAttack();
    }
    #endregion
}
