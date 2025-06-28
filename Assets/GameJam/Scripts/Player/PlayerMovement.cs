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
    public bool isGround = true;
    bool isMovable = true;

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
        if(isMovable)
        {
            playerRigid.velocity = new Vector2(moveInput * speed, playerRigid.velocity.y);
            OnMove?.Invoke(moveInput);
        }
    }

    public void ReadyJump()
    {
        isMovable = false;
        OnJump?.Invoke();
    }

    public void StartJump()
    {
        playerRigid.AddForce(transform.up * jumpForce);
        isMovable = true;
    }

    public void SetIsMovable(bool Movable)
    {
        isMovable = Movable;
    }
}
