using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [SerializeField]
    Goal goal;

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
        if (Input.GetKeyDown(KeyCode.Space) && playerAnimationController) playerMovement.ReadyJump();
        if (Input.GetKeyDown(KeyCode.E)) playerEquipper.Equip();
        if (Input.GetMouseButtonDown(0) /*&& !EventSystem.current.IsPointerOverGameObject()*/) playerAttack.Attack();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" && !playerAnimationController.IsStateName("Jump"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 위쪽에서 닿았는지 확인
                if (contact.normal.y >= 0.7f) // y값이 클수록 위쪽에서 충돌
                {
                    playerAnimationController.HandleLand();
                    playerMovement.isJumping = false;
                    return;
                }
            }
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
        if (collision.tag == "Monster" || collision.tag == "Trap")
        {
            SoundManager.Instance.PlaySound(SoundEnum.Hitted_Player);
            playerStatus.ChangeDurability(-1);
            gameObject.layer = 11;
            if (playerStatus.GetDurability() < 0)
            {
                isDead = true;
                Rigidbody2D rigid = GetComponent<Rigidbody2D>();
                playerMovement.SetIsMovable(false);
                rigid.velocity = Vector3.zero;
                goal.ActiveGameFailUI();
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


