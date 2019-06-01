using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBlow : MonoBehaviour
{
    public enum SkillType
    {
        Melee,
        RangeForward,
        RangeFollow
        
    }
    public SkillType skillType;

    private int damage = 10;

    private Transform pawnPoint;
    private Transform target;
    private Transform posAim;
    private float flySpeed;
    private bool isDisable;
    //private bool isFollow;
    private Vector3 vecDelta;
    private Vector3 vecGoto;
    private bool isInitlized = false;
    void Start()
    {
        isDisable = false;
        //isFollow = false;
    }
    
    void Update()
    {
        if (!isInitlized) return;
        switch(skillType)
        {
            case SkillType.Melee:
                {
                    UpdateMoveTo();
                    break;
                }                
            case SkillType.RangeForward:
                {
                    UpdateMoveForward();
                    break;
                }                
            case SkillType.RangeFollow:
                {
                    UpdateMoveToFollow();
                    break;
                }                
            default:
                Destroy(this.gameObject);
                Debug.LogError(string.Format("skill type [{0}] is null!", this.name));
                break;

        }
     
    }
    public void InitSkillBlow(Transform _pawnPoint, Transform _target,Transform _posAim, float _flySpeed,int _damage, SkillType _skillType)
    {
        isDisable = false;
        skillType = _skillType;
        pawnPoint = _pawnPoint;
        target = _target;
        posAim = _posAim;
        flySpeed = _flySpeed;
        damage = _damage;
        //vecDelta = pawnPoint.position;
        vecDelta = new Vector3(pawnPoint.position.x, pawnPoint.position.y, pawnPoint.position.z);
        vecGoto = new Vector3(_posAim.position.x, _posAim.position.y, _posAim.position.z);

        transform.LookAt(_posAim);
        isInitlized = true;
    }
    private void UpdateMoveToFollow()
    {
        transform.position = Vector3.MoveTowards(transform.position, this.posAim.position, this.flySpeed * Time.deltaTime);
        Vector3 _look = this.posAim.position - vecDelta;
        transform.rotation = Quaternion.LookRotation(_look);
    }
    private void UpdateMoveTo()
    {
        //Debug.LogError(this.flySpeed);
        transform.position = Vector3.MoveTowards(transform.position,this.vecGoto, this.flySpeed * Time.deltaTime);
        Vector3 _look = this.vecGoto - vecDelta;
        transform.rotation = Quaternion.LookRotation(_look);
    }

    private void UpdateMoveForward()
    {
        transform.position += transform.forward * this.flySpeed * Time.deltaTime;
        if ((Vector3.Distance(vecGoto, vecDelta) * 3) <= Vector3.Distance(vecDelta, this.transform.position))
        {
            Destroy(this.gameObject);
            //isDisable = true;
            //gameObject.SetActive(false);
            //Destroy(this.gameObject,2.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDisable)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isDisable = true;
                Destroy(gameObject);                
                SkillHit(other);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isDisable)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isDisable = true;
                Destroy(gameObject);
                SkillHit(collision.collider);
            }
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
