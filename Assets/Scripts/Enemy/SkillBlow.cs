using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBlow : MonoBehaviour
{
    private Transform PawnPoint;
    private Transform Target;
    private Transform TargetAim;
    private float FlySpeed;
    private bool isDisable;
    private bool isFollow;
    private Vector3 vecDelta;
    void Start()
    {
        isDisable = false;
        isFollow = false;
    }
    
    void Update()
    {
        if (Target != null && PawnPoint != null)
        {
            if (isFollow)
                MoveToFollow(Target, TargetAim, FlySpeed);
            else
                MoveTo(TargetAim, FlySpeed);
        }
        else
        {
            Destroy(this.gameObject);
        }       
    }
    public void InitSkillBlow(Transform _pawnPoint, Transform _target,Transform _targetAim,float flySpeed,bool _isFollow)
    {
        if (!_pawnPoint || !_target || !_targetAim)
        {
            Destroy(this.gameObject);
            return;
        }
        Target = _target;
        FlySpeed = flySpeed;
        PawnPoint = _pawnPoint;
        TargetAim = _targetAim;
        vecDelta = PawnPoint.position;
        isFollow = _isFollow;
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
    private void MoveTo(Transform movePos, float flySpeed)
    {
        if (!movePos)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos.position, flySpeed = flySpeed * Time.deltaTime);
            Vector3 _look = movePos.position - vecDelta;
            transform.rotation = Quaternion.LookRotation(_look);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isDisable)
        {
            isDisable = true;
            Destroy(gameObject, 1);

            //Debug.LogError("triger");
            //if (Target != null)
            //{                
            //    if (other.gameObject == Target.gameObject)
            //    {
            //        //var _health = other.gameObject.GetComponent<Health>();
            //        //if(_health)
            //        //{
            //        //    _health.TakeDamage(10);
            //        //}
            //        isDisable = true;
            //        Destroy(gameObject, 1);
            //    }
            //}
            //else
            //{
            //    isDisable = true;
            //    Destroy(this.gameObject);
            //}
        }
    }
    
}
