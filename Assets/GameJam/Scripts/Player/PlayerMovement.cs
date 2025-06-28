using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D playerRigid;

    private float moveInput;
    public float speed = 5.0f;
    public float jumpForce = 600.0f;
    bool isGround = true;
    bool isMove = true;

    public event Action<float> OnMove;
    public event Action OnJump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetMoveInput(float input)
    {
        moveInput = input;
    }

    private void Move()
    {
        if(isMove)
        {
            playerRigid.velocity = new Vector2(moveInput * speed, playerRigid.velocity.y);
            OnMove?.Invoke(moveInput);
        }
    }

    public void ReadyJump()
    {
        isMove = false;
        OnJump?.Invoke();
    }

    public void StartJump()
    {
        playerRigid.AddForce(transform.up * jumpForce);
        isMove = true;
    }
}
