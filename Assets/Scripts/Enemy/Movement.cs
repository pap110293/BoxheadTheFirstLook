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
    public void Init(TypeMove _typeMove, float _speed, float _flyHeight)
    {
        speed = _speed;
        flyHeight = _flyHeight;
        typeMove = _typeMove;
        if (navMeshAgent != null)
        {
            switch (typeMove)
            {
                case TypeMove.Walk:
                    navMeshAgent.enabled = true;
                    break;
                case TypeMove.Fly:
                    navMeshAgent.enabled = false;
                    break;
                default:
                    break;
            }
        }
    }
    public void SetMove(Transform _target, TypeMove _typeMove, float _speed,float _flyHeight)
    {
        target = _target;
        speed = _speed;
        flyHeight = _flyHeight;
        typeMove = _typeMove;
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
        switch (typeMove)
        {
            case TypeMove.Walk:
                navMeshAgent.SetDestination(this.transform.position);
                break;
            case TypeMove.Fly:
                break;
            default:
                break;
        }
        
    }
    void Update()
    {
        //if (flyHeight > 0)
        if(typeMove == TypeMove.Fly)
        {
            model.localPosition = new Vector3(0, flyHeight, 0);
            if (target != null)
            {
                var targetLookat = new Vector3(target.position.x, transform.position.y, target.position.z);
                transform.LookAt(targetLookat);
                //model.LookAt(target);
            }
        }
        
    }
}
