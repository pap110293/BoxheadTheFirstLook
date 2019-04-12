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

    private Vector3 vecDelta;
    void Start()
    {
        isDisable = false;
    }
    
    void Update()
    {
        if (Target != null && PawnPoint != null)
        {
            MoveTo(Target, TargetAim, FlySpeed);
        }
        else
        {
            Destroy(this.gameObject);
        }       
    }
    public void InitArrow(Transform pawnPoint, Transform target,Transform targetAim,float flySpeed)
    {
        if (!pawnPoint || !target || !targetAim)
        {
            Destroy(this.gameObject);
            return;
        }
        Target = target;
        FlySpeed = flySpeed;
        PawnPoint = pawnPoint;
        TargetAim = targetAim;
        vecDelta = PawnPoint.position;
    }
    private void MoveTo(Transform target, Transform targetAim, float flySpeed)
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
    private void OnTriggerEnter(Collider other)
    {
        if(!isDisable)
        {
            if (Target != null)
            {                
                if (other.gameObject == Target.gameObject)
                {
                    //var _health = other.gameObject.GetComponent<Health>();
                    //if(_health)
                    //{
                    //    _health.TakeDamage(10);
                    //}
                    isDisable = true;
                    Destroy(gameObject, 1);
                }
            }
            else
            {
                isDisable = true;
                Destroy(this.gameObject);
            }
        }
    }
    
}
