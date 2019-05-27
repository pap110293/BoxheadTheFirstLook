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
    public Transform model;
    private float flyHeight;
    private TypeMove typeMove;
    private float speed;    
    private NavMeshAgent navMeshAgent;
    private Transform target;
    void Awake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    public void SetMove(Transform _target, TypeMove _typeMove, float _speed,float _flyHeight)
    {
        target = _target;
        speed = _speed;
        flyHeight = _flyHeight;
        switch (typeMove)
        {
            case TypeMove.Walk:
                navMeshAgent.speed = _speed;
                navMeshAgent.SetDestination(_target.position);
                break;
            case TypeMove.Fly:
                transform.position += transform.forward * _speed * Time.deltaTime;
                break;
            default:                
                break;
        }

        
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
