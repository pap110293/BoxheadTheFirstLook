using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{

    public float timeToExplose;
    public int damage = 100;
    public int radius = 10;
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
            var explosion = fx.GetComponent<Explosion>();
            explosion.SetDamage(damage);
            explosion.SetRadius(radius);
            Destroy(fx, 2.0f);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
