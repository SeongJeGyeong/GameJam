using UnityEngine;
using Spine.Unity;
public class SpineAnimatorBehaviour : StateMachineBehaviour
{
    [SerializeField]
    AnimationClip motion;
    string animationClip;
    [Header("스파인 모션 레이어")]
    public int layer = 0;
    public float timeScale = 1f;
    public bool isLoop;

    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState spineAnimationState;
    private Spine.TrackEntry trackEntry;
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
            isLoop = stateInfo.loop;
            trackEntry = spineAnimationState.SetAnimation(layer, animationClip, isLoop);
            trackEntry.TimeScale = timeScale;
        }
    }
}
