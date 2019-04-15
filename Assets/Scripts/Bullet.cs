using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject fxOnHitObject;
    public GameObject fxOnHitLife;
    public float effectTime = 5.0f;
    Vector3 prevPos;
    private void Start()
    {
        prevPos = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.position - prevPos;
        Ray ray = new Ray(transform.position, direction.normalized);
        RaycastHit[] hits = Physics.RaycastAll(ray, direction.magnitude);

        for (int i = 0; i < hits.Length; i++)
        {
            var hit = hits[i];
            GameObject fx;
            var life = hit.collider.GetComponent<Life>();
            if (life)
            {
                fx = Instantiate(fxOnHitLife, hit.point, hit.collider.transform.rotation);
            }
            else
            {
                fx = Instantiate(fxOnHitObject, hit.point, hit.collider.transform.rotation);
            }
            Destroy(fx, effectTime);
            gameObject.SetActive(false);
        }

        prevPos = transform.position;
    }
}
