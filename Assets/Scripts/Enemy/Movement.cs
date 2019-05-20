using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{
    public enum TypeMove
    {
        Walk,
        Fly
    }
    public TypeMove typeMove;
    public float speed;
    public float flyHeight;
    public Transform model;
    NavMeshAgent navMeshAgent;
    private Transform target;
    void Awake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    public void SetMove(Transform _target, TypeMove _typeMove, float speed)
    {

        switch (typeMove)
        {
            case TypeMove.Fly:
                transform.position += transform.forward * speed * Time.deltaTime;
                break;
            default:
                navMeshAgent.speed = speed;
                navMeshAgent.SetDestination(_target.position);
                break;
        }

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
            var targetLookat = new Vector3(target.position.x,transform.position.y,target.position.z);
            transform.LookAt(targetLookat);
           //model.LookAt(target);
        }
    }
}
