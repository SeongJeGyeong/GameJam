using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public event Action OnStartJump;
    public event Action<bool> OnMoveEnable;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Jump") && animator.GetBool("IsGround") && stateInfo.normalizedTime >= 1.0f)
        {
            OnStartJump?.Invoke();
            HandleFall();
        }
        else if(stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1.0f)
        {
            animator.SetBool("IsAttack", false);
            OnMoveEnable?.Invoke(true);
        }
    }

    public void HandleMove(float input)
    {
        if (input != 0)
        {
            float flipDir = (input > 0) ? 1f : -1f;
            transform.localScale = new Vector3(flipDir, 1, 1);
            animator.SetBool("IsMove", true);
        }
        else
        {
            animator.SetBool("IsMove", false);
        }
    }

    public void HandleReadyJump()
    {
        animator.SetBool("IsJump", true);
    }

    public void HandleFall()
    {
        animator.SetBool("IsGround", false);
    }

    public void HandleLand()
    {
        animator.SetBool("IsGround", true);
        animator.SetBool("IsJump", false);
    }

    public void HandleAttack(int attackType)
    {
        animator.SetBool("IsAttack", true);
        OnMoveEnable?.Invoke(false);
    }
}