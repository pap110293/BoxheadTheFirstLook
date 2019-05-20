using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public EnemyAtribute enemyAtribute;
    //
    public Transform target;    
    public Movement planeMovement;
    public EnemyAttacker enemyAttacker;
    public TeamManager teamManager;
    public EnemyAnimState animState;
    void Start()
    {
        enemyAtribute = this.GetComponent<EnemyAtribute>();
        if(!enemyAtribute)
        {
            Debug.LogError(string.Format("Enemy[{0}] is null Atribute!", this.gameObject.name));
            Destroy(this.gameObject);
        }
        planeMovement = this.GetComponent<Movement>();
        teamManager = this.gameObject.GetComponent<TeamManager>();
        enemyAttacker = this.GetComponent<EnemyAttacker>();
        enemyAttacker.InitAttacker(enemyAtribute.attackCooldown, enemyAtribute.skillFlySpeed, enemyAtribute.skillType);
        animState = this.GetComponent<EnemyAnimState>();
    }
    public void UpdateChasing()
    {
        if (target) planeMovement.SetMove(target, enemyAtribute.typeMove, enemyAtribute.movementSpeed, enemyAtribute.flyHeight);
        animState.SetAnim(EnemyAnimState.AnimState.RunForward);
    }
    public void StopChasing()
    {
        animState.SetAnim(EnemyAnimState.AnimState.Idle);
        planeMovement.Stop();
    }
    public void UpdateAttacking()
    {
        if(enemyAttacker.isValidAttack)
        {
            animState.SetAnim(EnemyAnimState.AnimState.DefaultAttack, enemyAttacker.actionAttackSpeed);
            enemyAttacker.EnemyUpdateTargetAttack(target);
        }else
        {
            animState.SetAnim(EnemyAnimState.AnimState.Idle, 1);
        }
        
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
