using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    AnimatorStateInfo stateInfo;

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

    public bool IsStateName(string name)
    {
        return stateInfo.IsName(name);
    }

    private void FixedUpdate()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Jump") && animator.GetBool("IsGround") && stateInfo.normalizedTime >= 1.0f)
        {
            OnStartJump?.Invoke();
            animator.SetBool("IsGround", false);
        }
        else if((stateInfo.IsName("MeleeAttack") || stateInfo.IsName("MagicAttack") || stateInfo.IsName("HandAttack")) && stateInfo.normalizedTime >= 1.0f)
        {
            animator.SetInteger("AttackType", 0);
            OnMoveEnable?.Invoke(true);
        }
        else if(stateInfo.IsName("Hurt") && stateInfo.normalizedTime >= 1.0f)
        {
            Debug.Log("Hurt Á¾·á");
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

    public void HandleLand()
    {
        animator.SetBool("IsGround", true);
        animator.SetBool("IsJump", false);
    }

    public void HandleAttack(int attackType)
    {
        animator.SetInteger("AttackType", attackType);
        OnMoveEnable?.Invoke(false);
    }

    public void HandleDamaged()
    {
        SkeletonAnimation skeleton = GetComponentInChildren<SkeletonAnimation>();
        Color originColor = skeleton.Skeleton.GetColor();
        originColor.a = 0.5f;
        skeleton.Skeleton.SetColor(originColor);
        //OnMoveEnable?.Invoke(false);
        animator.SetTrigger("IsHurted");
        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 6;
        //OnMoveEnable?.Invoke(true);
        SkeletonAnimation skeleton = GetComponentInChildren<SkeletonAnimation>();
        Color originColor = skeleton.Skeleton.GetColor();
        originColor.a = 1f;
        skeleton.Skeleton.SetColor(originColor);
    }

    public void Dead()
    {
        Debug.Log("»ç¸Á");
        animator.SetTrigger("IsDead");
        SkeletonAnimation skeleton = GetComponentInChildren<SkeletonAnimation>();
        Color originColor = skeleton.Skeleton.GetColor();
        originColor.a = 1f;
        skeleton.Skeleton.SetColor(originColor);
    }
}