using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public event Action OnStartJump;

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
        if (animator.GetBool("IsGround") && stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 1.0f)
        {
            OnStartJump?.Invoke();
            HandleFall();
        }
    }

    public void HandleMove(float input)
    {
        if(input != 0)
        {
            float FlipDir = (input > 0) ? 1f : -1f;
            transform.localScale = new Vector3(FlipDir, 1, 1);
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
}
