﻿using System;
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

    private bool reloading;
    private float timeTemp;
    private AudioSource audioSource;
    private Animator animator;

    [Header("Ammo")]
    public bool infinityAmmo;
    public int clipSize = 30;
    public int maxAmmo = 30;
    public int ammo = 30;
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
    public bool isGun = true;
    public float FOVZoom = 65;
    public bool HideWhenZoom = false;

    private void Awake()
    {
        reloading = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timeTemp = 0.0f;
    }

    public override void OnAction()
    {
        if (isGun)
        {
            if (ammo <= 0)
            {
                if (!Reload())
                {
                    Debug.Log("Het dan");
                }
            }

            if (!reloading || ammo <= 0)
            {
                if (timeTemp <= 0.0f)
                {
                    animator.SetTrigger("shoot");
                    ammo--;
                    base.OnAction();
                    timeTemp = FireRate;
                }
                else
                {
                    timeTemp -= Time.deltaTime;
                }
            }
        }
        else
        {
            if (fire1)
            {
                if (timeTemp <= 0.0f)
                {
                    animator.SetTrigger("shoot");
                    base.OnAction();
                    timeTemp = FireRate;
                }
                else
                {
                    timeTemp -= Time.deltaTime;
                }
            }
        }
    }

    public override void OnFire1()
    {
        base.OnFire1();
        OnAction();
    }

    public override void OnFire1Realse()
    {
        base.OnFire1Realse();
        timeTemp = 0;
    }

    public override bool Reload()
    {
        if (ammo >= clipSize || ammoHave == 0)
            return false;

        if (!reloading)
        {
            if (audioSource && SoundReload)
            {
                audioSource.PlayOneShot(SoundReload);
            }

            if (animator)
                animator.SetTrigger("reloading");
        }

        reloading = true;
        base.Reload();
        return true;
    }

    public override void ReloadComplete()
    {
        if (infinityAmmo)
        {
            ammo = clipSize;
        }
        else
        {
            var ammoWasUsed = clipSize - ammo;
            if (ammoHave > ammoWasUsed)
            {
                ammoHave -= ammoWasUsed;
                ammo = clipSize;
            }
            else
            {
                ammo += ammoHave;
                ammoHave = 0;
            }
        }
        base.ReloadComplete();
        reloading = false;
    }

    private void OnEnable()
    {
        animator.SetInteger("shoot_type", (int)Type);
    }
}