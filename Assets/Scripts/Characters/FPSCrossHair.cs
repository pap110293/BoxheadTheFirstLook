using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCrossHair : MonoBehaviour
{
    private GameObject weaponCamera;

    private void Start()
    {
        weaponCamera = GameObject.FindGameObjectWithTag("WeaponCamera");
    }

    private void Update()
    {
        if(Input.GetButton("Fire2"))
        {

        }
    }
}
