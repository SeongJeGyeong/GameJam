using Spine.Unity;
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
    [SerializeField]
    PlayerEquipment playerEquipment;
    [SerializeField]
    PlayerStatus playerStatus;

    void Start()
    {
        playerMovement.OnMove += playerAnimationController.HandleMove;
        playerMovement.OnJump += playerAnimationController.HandleReadyJump;
        playerMovement.OnHurted += playerAnimationController.HandleDamaged;
        playerAnimationController.OnStartJump += playerMovement.StartJump;
        playerAnimationController.OnMoveEnable += playerMovement.SetIsMovable;
        playerAttack.OnAttack += playerAnimationController.HandleAttack;
        playerEquipment.OnApplyAttackPower += playerStatus.SetAttackPower;
        playerEquipment.OnApplyDurability += playerStatus.SetDurability;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.SetMoveInput(Input.GetAxisRaw("Horizontal"));
        if(Input.GetKeyDown(KeyCode.Space)) playerMovement.ReadyJump();
        if(Input.GetKeyDown(KeyCode.E)) playerEquipper.Equip();
        if (Input.GetMouseButtonDown(0)) playerAttack.Attack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            playerMovement.isGround = true;
            playerAnimationController.HandleLand();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            playerMovement.isGround = false;
            playerAnimationController.HandleFall();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            playerEquipper.AddOverlappedItem(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            playerMovement.Knockback(new Vector2(collision.transform.position.x, collision.transform.position.y));
            gameObject.layer = 11;
            playerStatus.ChangeDurability(-1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            playerEquipper.RemoveOverlappedItem(collision.gameObject);
        }
    }
}


