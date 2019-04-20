using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLife : LifeBase
{
    public Transform target;
    public GameObject fxOndead;

    private Rigidbody rb;

    public override void OnDead()
    {
        base.OnDead();
        if (fxOndead)
        {
            var fx = Instantiate(fxOndead, transform.position, Quaternion.identity);
            Destroy(fx, 1.0f);
        }
        gameObject.SetActive(false);
    }

    public override void Init()
    {
        base.Init();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (transform)
        {
            var vec = (target.position - transform.position).normalized * 4.0f * Time.deltaTime;
            rb.MovePosition(transform.position + vec);
        }
    }
}
