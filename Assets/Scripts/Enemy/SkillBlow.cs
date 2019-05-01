using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBlow : MonoBehaviour
{
    public int damage = 10;

    private Transform PawnPoint;
    private Transform Target;
    private Transform TargetAim;
    private float FlySpeed;
    private bool isDisable;
    private bool isFollow;
    private Vector3 vecDelta;
    private Vector3 vecGoto;
    void Start()
    {
        isDisable = false;
        //isFollow = false;
    }
    
    void Update()
    {
        if (Target != null && PawnPoint != null)
        {
            if (isFollow)
                MoveToFollow(Target, TargetAim, FlySpeed);
            else
                MoveTo(Target, vecGoto, FlySpeed);
        }
        else
        {
            Destroy(this.gameObject);
        } 
        if(Vector3.Distance(vecGoto, vecDelta)<=Vector3.Distance(vecDelta,this.transform.position))
        {
            Destroy(this.gameObject);
        }
    }
    public void InitSkillBlow(Transform _pawnPoint, Transform _target,Transform _targetAim,float flySpeed)//Init bullet follow target
    {
        if (!_pawnPoint || !_target || !_targetAim)
        {
            Destroy(this.gameObject);
            return;
        }
        PawnPoint = _pawnPoint;
        Target = _target;
        TargetAim = _targetAim;
        FlySpeed = flySpeed;
        vecDelta = PawnPoint.position;
        isFollow = true;
    }
    public void InitSkillBlow(Transform _pawnPoint, Transform _target,Vector3 _posGoto, float flySpeed)//Init bullet don't follow target
    {
        if (!_pawnPoint || !_target)
        {
            Destroy(this.gameObject);
            return;
        }
        PawnPoint = _pawnPoint;
        Target = _target;
        vecGoto = _posGoto;
        FlySpeed = flySpeed;
        vecDelta = PawnPoint.position;
        isFollow = false;
    }
    private void MoveToFollow(Transform target, Transform targetAim, float flySpeed)
    {
        if(!target || !targetAim)
        {
            Destroy(this.gameObject);
        }else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetAim.position, flySpeed = flySpeed * Time.deltaTime);
            Vector3 _look = targetAim.position - vecDelta;
            transform.rotation = Quaternion.LookRotation(_look);
        }
    }
    private void MoveTo(Transform target, Vector3 movePos, float flySpeed)
    {        
        transform.position = Vector3.MoveTowards(transform.position, movePos, flySpeed = flySpeed * Time.deltaTime);
        Vector3 _look = movePos - vecDelta;
        transform.rotation = Quaternion.LookRotation(_look);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isDisable && other.gameObject != gameObject)
        {
            isDisable = true;
            Destroy(gameObject);
            SkillHit(other);
        }
    }

    private void SkillHit(Collider other)
    {
            DamagePackage dm = new DamagePackage
            {
                Damage = damage,
                Normal = Vector3.up,
                Direction = Vector3.zero,
                Position = other.transform.position,
            };

        other.SendMessage("OnHit", dm, SendMessageOptions.DontRequireReceiver);
    }
}
