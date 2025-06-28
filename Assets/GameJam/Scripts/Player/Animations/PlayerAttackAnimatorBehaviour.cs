using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimatorBehaviour : StateMachineBehaviour
{
    [SerializeField]
    List<AnimationClip> motion;
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
            animationClip = motion[0].name;
        }
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (skeletonAnimation == null)
        {
            skeletonAnimation = animator.GetComponentInChildren<SkeletonAnimation>();
            spineAnimationState = skeletonAnimation.state;
        }

        PlayerEquipment equipInfo = animator.GetComponent<PlayerEquipment>();
        EquippedWeapon weapon = equipInfo.GetEquipList().leftHand;

        switch(weapon.type)
        {
            case GlobalEnums.ItemType.MELEE:
                animationClip = motion[0].name;
                break;
            case GlobalEnums.ItemType.STAFF:
                animationClip = motion[1].name;
                break;
            case GlobalEnums.ItemType.EMPTY:
                animationClip = motion[2].name;
                break;
        }

        if (animationClip != null)
        {
            trackEntry = spineAnimationState.SetAnimation(layer, animationClip, stateInfo.loop);
            trackEntry.TimeScale = weapon.attackSpeed;
            animator.speed = weapon.attackSpeed + 0.3f;
        }
    }
}
