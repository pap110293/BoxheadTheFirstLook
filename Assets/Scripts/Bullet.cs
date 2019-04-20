using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject fxOnHitObject;
    public GameObject fxOnHitLife;
    public float force;
    public float effectTime = 5.0f;

    public float Speed = 100;
    public float lifeTime = 1;

    private int damage = 1;

    Vector3 nextPos;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        UpdateBullet();
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void UpdateBullet()
    {
        var currentPos = transform.position;
        nextPos = transform.position + transform.forward * Speed * Time.deltaTime;
        Vector3 direction = nextPos - currentPos;
        Ray ray = new Ray(currentPos, direction.normalized);
        RaycastHit[] hits = Physics.RaycastAll(ray, direction.magnitude);

        for (int i = 0; i < hits.Length; i++)
        {
            var hited = hits[i];

            if (hited.collider.GetComponent<Rigidbody>())
                hited.collider.GetComponent<Rigidbody>().AddForceAtPosition((hited.transform.position - transform.position).normalized * force, hited.point);

            DamagePackage dm = new DamagePackage
            {
                Damage = damage,
                Normal = hited.normal,
                Direction = (hited.transform.position - transform.position).normalized * force,
                Position = hited.point,
            };

            //GameObject fx;
            //var life = hited.collider.GetComponent<Life>();
            //var rotation = Quaternion.LookRotation(hited.normal);
            //var hitMask = hited.collider.GetComponent<HitMark>();
            //if (hitMask)
            //{
            //    var fxObject = hitMask.HitFX;
            //    fx = Instantiate(fxObject, hited.point, rotation);
            //    Destroy(fx, effectTime);
            //}

            hited.collider.GetComponent<Collider>().SendMessage("OnHit", dm, SendMessageOptions.DontRequireReceiver);

            gameObject.SetActive(false);
        }

        transform.position = nextPos;
    }

}
