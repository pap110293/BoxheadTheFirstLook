using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{
    public float flyDictance;
    NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    public void SetMove(Transform _target)
    {
        navMeshAgent.SetDestination(_target.position);
    }
    public void Stop()
    {
        navMeshAgent.SetDestination(this.transform.position);
    }
    void Update()
    {
        if (flyDictance > 0) 
        {
            this.transform.position = new Vector3(this.transform.position.x, flyDictance, this.transform.position.z);
        }        
    }
}
