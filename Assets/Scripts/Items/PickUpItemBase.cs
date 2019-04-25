using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpItemBase : MonoBehaviour
{
    public float radius = 0.5f;
    private void Start()
    {
        InvokeRepeating("checkRadius", 0.0f, 0.2f);
    }

    void checkRadius()
    {
        var objects = Physics.OverlapSphere(transform.position, radius);
        foreach (var ob in objects)
        {
            if (ob.CompareTag("Player"))
            {
                ExecuteItem();
                Destroy(gameObject);
            }
        }
    }

    public virtual void ExecuteItem()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
