using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public enum EnemyType
    {
        Knight,
        Mage,
        Bat,
        Dragon
    }
    public EnemyData enemyAtribute;
    //
    public EnemyLife life;
    public Transform target;    
    public Movement enemyMovement;
    public EnemyAttacker enemyAttacker;
    public TeamManager teamManager;
    public EnemyAnimState animState;
    void Start()
    {
        //enemyAtribute = new EnemyAtribute();
        //enemyAtribute.SetAtribute(1, 0, 0, 0, 0, 0);
        #region Init Life
        life = this.GetComponent<EnemyLife>();
        life.maxHP = enemyAtribute.HP;
        life.maxArmor = enemyAtribute.Amor;
        life.Init();
        #endregion

        #region Init Movement
        enemyMovement = this.GetComponent<Movement>();
        enemyMovement.Init(enemyAtribute.typeMove, enemyAtribute.movementSpeed, enemyAtribute.flyHeight);
        enemyMovement.model.localScale = new Vector3(1+(enemyAtribute.level *0.1f), 1 + (enemyAtribute.level * 0.1f), 1 + (enemyAtribute.level * 0.1f));
        #endregion

        #region Init Attacker
        enemyAttacker = this.GetComponent<EnemyAttacker>();
        enemyAttacker.InitAttacker(enemyAtribute.attackCooldown, enemyAtribute.skillFlySpeed, enemyAtribute.damage, enemyAtribute.skillType);
        #endregion

        teamManager = this.gameObject.GetComponent<TeamManager>();
        animState = this.GetComponent<EnemyAnimState>();
    }
    public void UpdateChasing()
    {
        if (enemyAttacker.timeCastingSkill > 0) return;
        if (target) enemyMovement.SetMove(target, enemyAtribute.typeMove, enemyAtribute.movementSpeed, enemyAtribute.flyHeight);
        animState.SetAnim(EnemyAnimState.AnimState.RunForward);
    }
    public void StopChasing()
    {
        animState.SetAnim(EnemyAnimState.AnimState.Idle);
        enemyMovement.Stop();
    }
    public void UpdateAttacking()
    {
        //if(enemyAttacker.isValidAttack)
        //{
        //    animState.SetAnim(EnemyAnimState.AnimState.DefaultAttack, enemyAttacker.actionAttackSpeed);
        //    enemyAttacker.EnemyUpdateTargetAttack(target);
        //}else
        //{
        //    animState.SetAnim(EnemyAnimState.AnimState.Idle, 1);
        //}
        enemyAttacker.UpdateAtacker(target,animState);


    }
    #region Attack    
    private void Update()
    {
        if (teamManager != null) target = teamManager.target;
    }
    //public void DefaultAttackEvent()
    //{
    //    enemyAttacker.ActionAttack();
    //}
    #endregion
}
