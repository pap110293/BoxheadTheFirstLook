using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimState : MonoBehaviour
{
    public enum AnimState
    {
        Dead=-1,
        Idle,
        RunForward,
        MeleeAttack
        
    }
    public AnimState currentAnimState = AnimState.Idle;
    public Animator animator;
    public float animSpeed = 1;
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    void Update()
    {
        if (!!animator)
        {
            animator.speed = animSpeed;
            animator.SetInteger("AnimState", (int)currentAnimState);
        } 
    }
    public void SetAnim(AnimState _animState, float _animSpeed = 1)
    {
        currentAnimState = _animState;
        animSpeed = _animSpeed;
    }
}
