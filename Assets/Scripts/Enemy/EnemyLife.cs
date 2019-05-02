using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : LifeBase
{
    public GameObject fxOndead;
    public override void OnDead()
    {
        base.OnDead();
        Destroy(gameObject);
        gameObject.SetActive(false);
        if (fxOndead)
        {
            var fx = Instantiate(fxOndead, transform.position, Quaternion.identity);
            Destroy(fx, 1.0f);
            var item = MasterManager.itemSpawnerController.GetItem();
            if (item)
            {
                var itemInstance = Instantiate(item, transform.position, Quaternion.identity);
            }
        }
    }
}
