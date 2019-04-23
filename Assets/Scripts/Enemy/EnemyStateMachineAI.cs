using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachineAI : MonoBehaviour
{
    public float bowRange;
    public float dangerRange;
    public float meleeRange;
    public EnemyManager enemyManager;
    public Transform target;
    public EnemyFSM sfm = new EnemyFSM();
    private string curAction;
    public TeamManager teamManager;
    void Start()
    {
        teamManager = this.gameObject.GetComponent<TeamManager>();
        enemyManager = this.gameObject.GetComponent<EnemyManager>();
        sfm.Init(bowRange, dangerRange, meleeRange, enemyManager);
    }
    void Update()
    {
        if (teamManager != null) target = teamManager.target;
        if (!target)
        {
            enemyManager.StopChasing();
            return;
        }
        var distance = Vector3.Distance(transform.position, target.position);

        sfm.PlayerDistance = distance;
        sfm.IsValidAttack = CheckAvailableAttack();
        sfm.Update();
    }

    void Hit()
    {
    }

    void FootR()
    {
    }

    void FootL()
    {
    }

    private void OnDrawGizmos()
    {
        var center = transform.position;
        center.y += 0.01f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, bowRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, dangerRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(center, meleeRange);
    }
    private bool CheckAvailableAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, target.transform.position - this.transform.position, out hit,1000))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.LogWarning("Did Hit:");
            //if (!!hit.transform.gameObject.GetComponent<EnemyManager>())
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                return true;
            }
            return false;
        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.LogWarning("Did not Hit");
            return true;
        }
    }
}
