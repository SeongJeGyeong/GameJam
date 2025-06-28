using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimatorBehaviour : SpineAnimatorBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        PlayerEquipment equipInfo = animator.GetComponent<PlayerEquipment>();
        base.trackEntry.TimeScale = equipInfo.GetEquipList().leftHand.attackSpeed;
    }
}
