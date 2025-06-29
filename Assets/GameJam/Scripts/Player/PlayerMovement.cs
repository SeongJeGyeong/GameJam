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
    public event Action OnHurted;
    public event Action OnGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(playerRigid.position, Vector2.down * 0.5f, new Color(1, 0, 0));
        RaycastHit2D hit = Physics2D.Raycast(playerRigid.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        //if(playerRigid.velocity.y <= 0)
        //{
        //    OnGrounded?.Invoke();
        //}

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
            //Debug.Log("velocity : " + playerRigid.velocity.x + ", " + playerRigid.velocity.y);
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

    public void Knockback(Vector2 hittedPos)
    {
        SetIsMovable(false);
        playerRigid.velocity = Vector2.zero;
        int dir = transform.position.x - hittedPos.x > 0 ? 1 : -1;
        playerRigid.AddForce(new Vector2(dir, 1) * 5, ForceMode2D.Impulse);
        OnHurted?.Invoke();
    }
}
