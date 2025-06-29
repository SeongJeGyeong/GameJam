using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    bool isDead = false;

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
        playerStatus.OnDead += playerAnimationController.Dead;

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        playerMovement.SetMoveInput(Input.GetAxisRaw("Horizontal"));
        if(Input.GetKeyDown(KeyCode.Space)) playerMovement.ReadyJump();
        if(Input.GetKeyDown(KeyCode.E)) playerEquipper.Equip();
        if (Input.GetMouseButtonDown(0)) playerAttack.Attack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // ���ʿ��� ��Ҵ��� Ȯ��
                if (contact.normal.y >= 0.7f) // y���� Ŭ���� ���ʿ��� �浹
                {
                    Debug.Log("���� �浹");
                    playerAnimationController.HandleLand();
                    return;
                }
            }
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "Ground")
    //    {
    //        playerAnimationController.HandleFall();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            playerEquipper.AddOverlappedItem(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster" || collision.tag == "Trap")
        {
            playerStatus.ChangeDurability(-1);
            gameObject.layer = 11;
            if (playerStatus.GetDurability() < 0)
            {
                isDead = true;
            }
            else
            {
                if (playerStatus.GetDurability() == 0) playerEquipment.SetArmorChange(new Equipment());
                playerMovement.Knockback(new Vector2(collision.transform.position.x, collision.transform.position.y));
            }
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


