using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{
    public float flyHeight;
    public Transform model;

    NavMeshAgent navMeshAgent;
    private Transform target;

    void Awake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    public void SetMove(Transform _target)
    {
        navMeshAgent.SetDestination(_target.position);
        target = _target;
    }
    public void Stop()
    {
        navMeshAgent.SetDestination(this.transform.position);
    }
    void Update()
    {
        if (flyHeight > 0) 
        {
            model.localPosition = new Vector3(0, flyHeight, 0);
        }
        if (target != null)
        {
            transform.LookAt(target);
            model.LookAt(target);
        }
    }
}
