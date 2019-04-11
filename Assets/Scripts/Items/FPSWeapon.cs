using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FPSWeapon : FPSItem
{
    public enum UsingTypes
    {
        FastShooting,
        SlowShooting,
        Mele,
        Tool
    }

    private float timeTemp;
    private AudioSource audioSource;
    private Animator animator;
    private bool reloading;

    [Header("Ammo")]
    public bool infinityAmmo;
    public int clipSize = 30;
    public int currentAmmo = 30;
    public int ammoMax = 30;
    [HideInInspector]
    public int ammoHave = 0;

    [Header("Firing")]
    public UsingTypes Type = 0;
    public float FireRate = 0.09f;
    public byte Spread = 20;
    public byte Damage = 10;
    public Vector2 KickPower = Vector2.zero;
    public Vector3 AimPosition = new Vector3(-0.082f, 0.06f, 0);

    [Header("Sound / FX")]
    public AudioClip SoundFire;
    public AudioClip SoundReload;
    public GameObject MuzzleFX;
    public Transform MuzzlePoint;

    [Header("Other")]
    public float FOVZoom = 65;
    public bool HideWhenZoom = false;

    private void Awake()
    {
        reloading = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timeTemp = 0.0f;
    }

    private void Start()
    {
        animator.SetInteger("shoot_type", (int)Type);
    }

    // Update is called once per frame
    void Update()
    {
        if(CrossPlatformInputManager.GetButton("Fire1"))
        {
            if (timeTemp <= 0.0f)
            {
                OnAction();
                timeTemp = FireRate;
            }
            else
            {
                timeTemp -= Time.deltaTime;
            }
        }
        else
        {
            timeTemp = 0;
        }
    }

    public override void OnAction()
    {
        animator.SetTrigger("shoot");
        base.OnAction();
    }
}
