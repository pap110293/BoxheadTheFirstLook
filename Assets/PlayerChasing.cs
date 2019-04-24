using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChasing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var movement = GetComponent<Movement>();
        movement.SetMove(player.transform);
    }
}
