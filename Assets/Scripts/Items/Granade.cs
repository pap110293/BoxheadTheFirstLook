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
            var fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(fx, 2.0f);
        }
        Destroy(gameObject);
    }
}
