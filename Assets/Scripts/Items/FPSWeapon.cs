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

    public enum ShootType
    {
        Normal,
        ShotGun
    }


    public enum WeaponType
    {
        Gun,
        Tool,
    }

    private bool reloading;
    private float timeTemp;
    private AudioSource audioSource;
    private Animator animator;
    private bool isScoped = false;

    public float animationSpeed = 1.0f;
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
    public int Damage = 10;

    [Header("Sound / FX")]
    public AudioClip SoundFire;
    public AudioClip SoundReload;
    public AudioClip soundClipOut;
    public GameObject MuzzleFX;
    public Transform[] MuzzlePoint;
    public GameObject bulletPrefab;
    public Transform point;


    [Header("Other")]
    public WeaponType weaponType;
    public float FOVZoom = 65;
    public bool canScope = false;

    private void Awake()
    {
        reloading = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timeTemp = 0.0f;
    }

    private void Start()
    {
        if (animator)
        {
            animator.speed = animationSpeed;
        }
    }

    private void Update()
    {
        timeTemp -= Time.deltaTime;

        if (Input.GetButtonDown("Fire2") && canScope)
        {
            isScoped = !isScoped;
            if (isScoped)
                OnScoped();
            else
                OnUnScoped();
        }
    }

    private void OnUnScoped()
    {
        MasterManager.gameHUBCanvas.UnScoped();
    }

    private void OnScoped()
    {
        MasterManager.gameHUBCanvas.Scoped(FOVZoom);
    }

    private void MakeToolHit()
    {
        if (fire1)
        {
            if (timeTemp <= 0.0f)
            {
                animator.SetTrigger("shoot");
                timeTemp = FireRate;
                
            }
        }
    }

    private void MakeGunShoot()
    {
        if (ammo <= 0)
        {
            if (!Reload() && audioSource && soundClipOut)
            {
                audioSource.PlayOneShot(soundClipOut);
            }
        }

        if (!reloading && ammo > 0)
        {
            if (timeTemp <= 0.0f)
            {
                if (SoundFire && audioSource)
                {
                    audioSource.PlayOneShot(SoundFire);
                }
                animator.SetTrigger("shoot");
                CreateMuzzleFX();
                ShootTheBullet();
                ammo--;
                timeTemp = FireRate;
            }
        }
    }

    private void ShootTheBullet()
    {
        if (bulletPrefab && point)
        {
            var bullet = Instantiate(bulletPrefab, point.position, point.rotation);
            var bulletClass = bullet.GetComponent<Bullet>();
            bulletClass.SetDamage(Damage);
        }
    }

    private void CreateMuzzleFX()
    {
        if (MuzzleFX && MuzzlePoint.Length > 0)
        {
            foreach (var point in MuzzlePoint)
            {
                var fx = Instantiate(MuzzleFX, point.position, point.rotation);
                fx.GetComponent<ParticleSystem>().Play();
                Destroy(fx, 2.0f);
            }
        }
    }

    public override void OnFire1()
    {
        base.OnFire1();
        switch (weaponType)
        {
            case WeaponType.Gun:
                MakeGunShoot();
                break;
            case WeaponType.Tool:
                MakeToolHit();
                break;
            default:
                break;
        }
    }

    public override void OnFire1Realse()
    {
        base.OnFire1Realse();
    }

    public override void OnAction()
    {
        base.OnAction();
        if (weaponType == WeaponType.Tool)
        {
            if(audioSource && SoundFire)
            {
                audioSource.PlayOneShot(SoundFire);
            }

            var bullet = Instantiate(bulletPrefab, point.position, point.rotation);
            bullet.GetComponent<TrailRenderer>().enabled = false;
            var bulletClass = bullet.GetComponent<Bullet>();
            bulletClass.SetDamage(Damage);
            bulletClass.SetDistance(0.01f);
        }
    }

    public override bool Reload()
    {
        if ((ammo >= clipSize || ammoHave == 0) && !infinityAmmo)
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
        if (SoundReload && audioSource)
        {
            audioSource.PlayOneShot(SoundReload);
        }

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
        MasterManager.gameHUBCanvas.UnScoped();
        reloading = false;
    }

    public Delegate upgrate;

    public void AddAmmor(int amount)
    {
        ammoHave += amount;
        if (ammoHave > maxAmmo)
            ammoHave = maxAmmo;
    }

    public void AddDamage(int amount)
    {
        Damage += amount;
    }
}
