using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSItemToThrow : FPSItem
{
    public GameObject Item;
    public AudioClip SoundThrow;
    public bool infinity;
    public float force = 10.0f;
    public Transform point;

    private AudioSource audioSource;
    private Animator animator;
    private int throwType = 4;
    private bool isThrow = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        animator.SetInteger("shoot_type", throwType);
    }

    public override void OnFire1()
    {
        base.OnFire1();
        if (!isThrow && amount > 0)
        {
            animator.SetTrigger("shoot");
            isThrow = true;
        }
    }

    public override void OnFire1Realse()
    {
        base.OnFire1Realse();
        animator.speed = 1.0f;
    }

    public void StopAnim()
    {
        animator.speed = 0.0f;
    }

    public override void OnAction()
    {
        base.OnAction();

        if(isThrow)
        {
            var i = Instantiate(Item, point.position, point.rotation);
            var rb = i.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * force, ForceMode.Force);
            if(!infinity)
                amount--;
        }

        if(amount <= 0)
        {
            var fpsItemController = FindObjectOfType<FPSItemController>();
            fpsItemController.PrevItem();
        }
        isThrow = false;
    }
}
