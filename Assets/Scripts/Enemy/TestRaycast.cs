using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool x = CheckAvailableAttack();
    }
    private bool CheckAvailableAttack()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity /*, layerMask*/))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.LogWarning("Did Hit:");
            if (!!hit.transform.gameObject.GetComponent<EnemyManager>())
            {
                Debug.LogWarning("true----");
                return true;
            }
            return false;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);            
            Debug.LogWarning("Did not Hit");
            return true;
        }
    }
}
