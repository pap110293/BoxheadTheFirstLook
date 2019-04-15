using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{

    public float timeToExplose;
    public GameObject explosionFX;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explosion", timeToExplose);
    }

    private void Explosion()
    {
        if (explosionFX)
        {
            var rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            var fx = Instantiate(explosionFX, transform.position, rotation);
            Destroy(fx, 2.0f);
        }
        Destroy(gameObject);
    }
}
