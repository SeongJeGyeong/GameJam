using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerMovement playerMovement;
    [SerializeField]
    PlayerAnimationController playerAnimationController;
    [SerializeField]
    PlayerAttack playerAttack;
    [SerializeField]
    PlayerEquipper playerEquipper;

    public event Action OnFall;
    public event Action OnLand;

    void Start()
    {
        playerMovement.OnMove += playerAnimationController.HandleMove;
        playerMovement.OnJump += playerAnimationController.HandleReadyJump;
        playerAnimationController.OnStartJump += playerMovement.StartJump;
        OnFall += playerAnimationController.HandleFall;
        OnLand += playerAnimationController.HandleLand;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.SetMoveInput(Input.GetAxisRaw("Horizontal"));
        if(Input.GetKeyDown(KeyCode.Space)) playerMovement.ReadyJump();
        if (Input.GetKeyDown(KeyCode.E)) playerEquipper.Equip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
