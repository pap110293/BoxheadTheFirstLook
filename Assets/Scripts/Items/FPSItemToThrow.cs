using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSItemToThrow : FPSItem
{
    public bool HoldFire = false;
    public GameObject Item;
    public float FireRate = 0.1f;
    public int UsingType = 0;
    public ItemData ItemUsed;
    public bool InfinityAmmo;
    public bool OnAnimationEvent;
    public float Force1 = 15;
    public float Force2 = 5;
    public AudioClip SoundThrow;
    private CharacterSystem character;
    private FPSController fpsController;
    private float timeTemp;
    private AudioSource audioSource;
    private Animator animator;
    private int throwType = 0;


}
