using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject fxOnHitObject;
    public GameObject fxOnHitLife;
    public float effectTime = 5.0f;

    public float Speed = 100;
    public float lifeTime = 1;
    Vector3 nextPos;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        UpdateBullet();
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
            var hit = hits[i];
            GameObject fx;
            var life = hit.collider.GetComponent<Life>();
            var rotation = Quaternion.LookRotation(hit.normal);
            var hitMask = hit.collider.GetComponent<HitMark>();
            if (hitMask)
            {
                var fxObject = hitMask.HitFX;
                fx = Instantiate(fxObject, hit.point, rotation);
                Destroy(fx, effectTime);
            }
            gameObject.SetActive(false);
        }

        transform.position = nextPos;
    }
}
