using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUBManager : MonoBehaviour
{

    private GameObject weaponCamera;
    private Camera mainCamera;
    private float defaultFOV;

    public GameObject crossHair;
    public GameObject crossHairZoom;
    public Text hp,armor;

    private void Awake()
    {
        MasterManager.gameHUBCanvas = this;
        weaponCamera = GameObject.FindGameObjectWithTag("WeaponCamera");
        mainCamera = Camera.main;
        defaultFOV = mainCamera.fieldOfView;
    }

    public void Scoped(float FOVZoom)
    {
        crossHair.SetActive(false);
        crossHairZoom.SetActive(true);
        weaponCamera.SetActive(false);
        mainCamera.fieldOfView = FOVZoom;
    }

    public void UnScoped()
    {
        crossHair.SetActive(true);
        crossHairZoom.SetActive(false);
        weaponCamera.SetActive(true);
        mainCamera.fieldOfView = defaultFOV;
    }
}
