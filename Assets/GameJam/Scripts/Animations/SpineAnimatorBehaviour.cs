﻿using UnityEngine;
using Spine.Unity;
public class SpineAnimatorBehaviour : StateMachineBehaviour
{
    [SerializeField]
    AnimationClip motion;
    string animationClip;
    [Header("스파인 모션 레이어")]
    public int layer = 0;
    public float timeScale = 1f;

    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState spineAnimationState;
    protected Spine.TrackEntry trackEntry;
    private void Awake()
    {
        if (motion != null)
        {
            animationClip = motion.name;
        }
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (skeletonAnimation == null)
        {
            skeletonAnimation = animator.GetComponentInChildren<SkeletonAnimation>();
            spineAnimationState = skeletonAnimation.state;
        }

        if (animationClip != null)
        {
            if(animationClip == "MeleeAttack" || animationClip == "MagicAttack")
            {
                PlayerEquipment equipInfo = animator.GetComponent<PlayerEquipment>();
                EquippedWeapon weapon = equipInfo.GetEquipList().leftHand;
                timeScale = weapon.attackSpeed;
                animator.speed = weapon.attackSpeed + 0.3f;
            }

            trackEntry = spineAnimationState.SetAnimation(layer, animationClip, stateInfo.loop);
            trackEntry.TimeScale = timeScale;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animationClip == "Hurt")
        {
            PlayerMovement movement = animator.GetComponent<PlayerMovement>();
            movement.SetIsMovable(true);
        }
    }
}
