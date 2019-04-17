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
    public EnemyAttacker enemyAttacker;
    public TeamManager teamManager;
    void Start()
    {
        planeMovement = this.GetComponent<Movement>();
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
        //if(!animator) animator.SetInteger("AnimState", (int)AnimState.Idle);
        planeMovement.Stop();
    }
    public void UpdateAttacking()
    {
        transform.LookAt(target);
        //if (!animator) animator.SetInteger("AnimState", (int)AnimState.MeleeAttack);
        enemyAttacker.EnemyUpdateAttack(target);
    }
    #region Attack    
    private void Update()
    {
        if (teamManager != null) target = teamManager.target;
    }
    
    #endregion
}
